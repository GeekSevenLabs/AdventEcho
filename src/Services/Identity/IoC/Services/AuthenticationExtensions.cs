using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using AdventEcho.Identity.Application.Accounts.Refresh;
using AdventEcho.Identity.Infrastructure.Options;
using AdventEcho.Kernel.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Identity.IoC.Services;

public static class AuthenticationExtensions
{
    private static TokenValidationParameters AddSigningCredentialsAndCreateTokenValidationParameters(
        this IServiceCollection services, 
        AdventEchoIdentityOption configs)
    {
        var privateKey = Convert.FromBase64String(configs.Jwt.PrivateKey);
        var publicKey = Convert.FromBase64String(configs.Jwt.PublicKey);
        
        var rsaPrivate = RSA.Create();
        rsaPrivate.ImportRSAPrivateKey(privateKey, out _);

        var credentials = new SigningCredentials(new RsaSecurityKey(rsaPrivate), SecurityAlgorithms.RsaSha256);
        services.AddSingleton(credentials);

        var rsaPublic = RSA.Create();
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
        services.AddSingleton(parameters);
        
        return parameters;
    }
    
    public static void AddAdventEchoAuthenticationSchemas(this IServiceCollection services, AdventEchoIdentityOption configs)
    {
        
        var parameters = services.AddSigningCredentialsAndCreateTokenValidationParameters(configs);
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = parameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<TokenValidationParameters>>();
                        logger.LogWarning("Falha na autenticação JWT: {Exception}", context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            })
            .AddCookie(AdventEchoIdentityCookieDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = configs.Cookie.AccessTokenName;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                
                options.LoginPath = configs.Cookie.LoginPath;
                options.LogoutPath = configs.Cookie.LoginPath;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                
                // options.Cookie.Domain = ".geekseven.com.br"
                
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                
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
                        var refreshIdString = context.Request.Cookies[configs.Cookie.RefreshIdName];
                        if (string.IsNullOrEmpty(refreshIdString))
                        {
                            logger.LogWarning("Refresh token is null or empty");
                            context.RejectPrincipal();
                            return;
                        }

                        if (!Guid.TryParse(refreshIdString, out var refreshId))
                        {
                            logger.LogWarning("Refresh token is invalid");
                            context.RejectPrincipal();
                            return;
                        }

                        var handler = context
                            .HttpContext
                            .RequestServices
                            .GetRequiredService<IRefreshLoginHandler>();

                        var request = new RefreshLoginRequest
                        {
                            RefreshId = refreshId,
                            UseCookie = true
                        };
                        
                        var refreshLoginResult = await handler.HandleAsync(request);

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