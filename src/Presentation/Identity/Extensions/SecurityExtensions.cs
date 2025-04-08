using System.Security.Cryptography;
using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Identity.Infrastructure;
using AdventEcho.Identity.Infrastructure.Models;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Presentation.Identity.Extensions;

public static class SecurityExtensions
{
    public static WebApplicationBuilder AddAdventEchoIdentitySecurityServices(
        this WebApplicationBuilder builder,
        AdventEchoIdentityOptions options)
    {
        var privateKey = builder.Configuration["AdventEchoIdentity:Bearer:PrivateKey"];
        Throw.When.Null(privateKey, "AdventEchoIdentity:Bearer:PrivateKey is not set");
        
        var privateKeyBytes = Convert.FromBase64String(privateKey);
        var publicKeyBytes = Convert.FromBase64String(options.Bearer.PublicKey);
        
        var rsaPrivate = RSA.Create();
        rsaPrivate.ImportRSAPrivateKey(privateKeyBytes, out _);

        var credentials = new SigningCredentials(new RsaSecurityKey(rsaPrivate), SecurityAlgorithms.RsaSha256);
        builder.Services.AddSingleton(credentials);

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
        
        builder.Services
            .AddIdentityCore<AdventEchoUser>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                
                opt.SignIn.RequireConfirmedAccount = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.SignIn.RequireConfirmedPhoneNumber = false;
                
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 8;
            })
            .AddSignInManager()
            .AddRoles<Role>()
            .AddAdventEchoIdentityDbContextStores()
            .AddDefaultTokenProviders();

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
            var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediatorHandler>();
            try
            {
                var command = new RefreshLoginAccountRequest
                {
                    RefreshToken = refreshToken,
                    UseCookie = true
                };
                
                var tokensResult = await mediator.SendCommandAsync(command);

                if (tokensResult.IsFail(out var tokens, out _))
                {
                    context.Fail("Unauthorized");
                }
                else
                {
                    // Set the new access token in the request headers
                    context.Token = tokens.AccessToken.Value;
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