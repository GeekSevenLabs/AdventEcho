using System.Security.Cryptography;
using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Identity.Application.Shared.Services;
using AdventEcho.Kernel.Infrastructure.Extensions;
using AdventEcho.Kernel.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Presentation.Web.Server.Extensions;

public static class SecurityExtensions
{
    public static WebApplicationBuilder AddAdventEchoIdentitySecurityForClientServices(
        this WebApplicationBuilder builder,
        AdventEchoIdentityOptions options)
    {
        var publicKeyBytes = Convert.FromBase64String(options.Bearer.PublicKey);
        
        var rsaPublic = RSA.Create();
        rsaPublic.ImportRSAPublicKey(publicKeyBytes, out _);
        
        var parameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(rsaPublic),
            ValidateIssuer = true,
            ValidIssuer = options.Bearer.Issuer,
            ValidateAudience = true,
            ValidAudiences = options.Bearer.Audiences,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        
        builder.Services.AddSingleton(parameters);

        builder.Services
            .AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = parameters;
                opt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context => OnMessageReceived(context, options)
                };
            });

        builder.Services.AddAuthorization();
        
        return builder;
    }
    
    private static async Task OnMessageReceived(MessageReceivedContext context, AdventEchoIdentityOptions options)
    {
        // Check if the request contains a token in cookies
        if (context.Request.Cookies.TryGetValue(options.Cookie.AccessTokenName, out var accessToken))
        {
            // Set the token in the request headers
            context.Token = accessToken;
        }
        else if (context.Request.Cookies.TryGetValue(options.Cookie.RefreshTokenName, out var refreshToken))
        {
            // Check if the refresh token is null or empty
            if (string.IsNullOrEmpty(refreshToken)) return;
            
            // Refresh the tokens
            var service = context.HttpContext.RequestServices.GetRequiredService<IAccountViewService>();
            try
            {
                var command = new RefreshLoginAccountRequest
                {
                    RefreshToken = refreshToken,
                    UseCookie = true
                };

                var tokensResult = await service.RefreshLoginAsync(command, CancellationToken.None);

                if (tokensResult.IsFail(out var tokens, out var error))
                {
                    context.Fail(error);
                }
                else
                {
                    // Set the new access token in the request headers
                    context.Token = tokens.AccessToken.Value;
                    context.HttpContext.RemoveTokensFromCookie(options);
                    context.HttpContext.SetTokensIntoCookie(
                        options, 
                        tokens.AccessToken,
                        tokens.RefreshToken,
                        DateTimeOffset.UtcNow.AddMinutes(options.Bearer.RefreshTokenExpirationInMinutes - 10)
                    );
                }
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.Cookies.Delete(options.Cookie.AccessTokenName);
                context.HttpContext.Response.Cookies.Delete(options.Cookie.RefreshTokenName);
                context.Fail(ex);
            }
        }
    }
}