using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using AdventEcho.Identity.Application.Shared.Services;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Kernel.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Kernel.Infrastructure.Extensions;

public static class AuthenticationExtensions
{
    private static TokenValidationParameters AddSigningCredentialsAndCreateTokenValidationParameters(
        this WebApplicationBuilder builder,
        AdventEchoIdentityForClientOption configs)
    {
        var publicKey = Convert.FromBase64String(configs.Jwt.PublicKey);

        using var rsaPublic = RSA.Create();
        rsaPublic.ImportRSAPublicKey(publicKey, out _);
        var parameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(rsaPublic),
            ValidateIssuer = true,
            ValidIssuer = configs.Jwt.Issuer,
            ValidateAudience = true,
            ValidAudiences = configs.Jwt.Audiences,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        builder.Services.AddSingleton(parameters);

        return parameters;
    }

    public static void AddAdventEchoSecurityForClient(this WebApplicationBuilder builder)
    {
        var adventOptions = builder.AddAdventEchoIdentityForClientOptions();
        var parameters = builder.AddSigningCredentialsAndCreateTokenValidationParameters(adventOptions);
        
        builder.AddAdventEchoIdentityHttpClient(adventOptions);
        builder.AddAdventEchoAuthentication(adventOptions, parameters);
        builder.Services.AddAuthorization();
    }

    private static void AddAdventEchoIdentityHttpClient(this WebApplicationBuilder builder, AdventEchoIdentityForClientOption configs)
    {
        builder.Services.AddHttpClient(
            ApplicationConstants.HttpClients.AdventEchoIdentity,
            client => { client.BaseAddress = new Uri(configs.Domains.Identity); }
        );
    }
    
    private static void AddAdventEchoAuthentication(
        this WebApplicationBuilder builder,
        AdventEchoIdentityForClientOption configs,
        TokenValidationParameters parameters)
    {
        builder.Services
            .AddAuthentication(AdventEchoIdentityCookieDefaults.AuthenticationScheme)
            .AddCookie(AdventEchoIdentityCookieDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = configs.Cookie.AccessTokenName;
                // options.Cookie.Domain = ".geekseven.com.br";
                options.LoginPath = options.LoginPath;
                options.LogoutPath = options.LogoutPath;
                options.Events.OnValidatePrincipal = async context =>
                {
                    var token = context.Request.Cookies[configs.Cookie.AccessTokenName];
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<TokenValidationParameters>>();
                    
                    if (string.IsNullOrEmpty(token))
                    {
                        logger.LogWarning("Token is null or empty");
                        context.RejectPrincipal();
                        return;
                    }

                    var tokenHandler = new JwtSecurityTokenHandler();
                    try
                    {
                        var principal = tokenHandler.ValidateToken(token, parameters, out _);
                        context.Principal = principal;
                    }
                    catch (SecurityTokenExpiredException)
                    {
                        var refreshId = context.Request.Cookies[configs.Cookie.RefreshIdName];
                        if (string.IsNullOrEmpty(refreshId))
                        {
                            logger.LogWarning("Refresh token is null or empty");
                            context.RejectPrincipal();
                            return;
                        }

                        var service = context
                            .HttpContext
                            .RequestServices
                            .GetRequiredService<IAccountViewService>();

                        var refreshLoginResult = await service.RefreshLoginAsync();

                        refreshLoginResult.Switch(
                            onSuccess: response =>
                            {
                                context.Response.Cookies.Append(configs.Cookie.AccessTokenName, response.AccessToken.Value, new CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    SameSite = SameSiteMode.Strict,
                                    // Domain = ".geekseven.com.br",
                                    Expires = response.AccessToken.Expires
                                });
                                
                                context.Response.Cookies.Append(configs.Cookie.RefreshIdName, response.RefreshId.ToString(), new CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    SameSite = SameSiteMode.Strict,
                                    // Domain = ".geekseven.com.br",
                                    Expires = DateTime.UtcNow.AddDays(30)
                                });
                                
                                var newPrincipal = tokenHandler.ValidateToken(response.AccessToken.Value, parameters, out _);
                                context.Principal = newPrincipal;
                            }, 
                            _ => context.RejectPrincipal()
                        );
                    }
                };
            });
    }
}