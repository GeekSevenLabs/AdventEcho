using System.Text;
using AdventEcho.Identity.Application.Extensions;
using AdventEcho.Identity.Infrastructure.Extensions;
using AdventEcho.Identity.Infrastructure.Models;
using AdventEcho.Identity.Infrastructure.Options;
using AdventEcho.Kernel.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Resend;

namespace AdventEcho.Identity.IoC;

public static class DependencyInjection
{
    public static void AddAdventEchoIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        // Configurations
        services.AddConfigurations(configuration);
        
        // Infrastructure
        services.AddAdventEchoIdentityInfrastructure(configuration);
        
        // Applications
        services.AddAdventEchoIdentityApplicationServices();

        // Resend
        services.AddResend();
        
        // Identity
        services.AddIdentity();
        
        // Authentication and Authorization
        services.AddAuthenticationSchemas(configuration);
        services.AddAuthorization();
    }

    private static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();

        var domainConfiguration = configuration.GetSection(AdventEchoIdentityDomainsConfiguration.SectionName).Get<AdventEchoIdentityDomainsConfiguration>().Required();
        
        services.Configure<AdventEchoIdentityDomainsConfiguration>(options =>
        {
            options.ApiIdentity = domainConfiguration.ApiIdentity;
        });
        
        services.Configure<ResendClientOptions>(o =>
        {
            o.ApiToken = configuration["ResendKey"]!;
        });
    }
    
    private static void AddResend(this IServiceCollection services)
    {
        services.AddHttpClient<ResendClient>();
        services.AddTransient<IResend, ResendClient>();
    }
    
    private static void AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<Role>()
            .AddAdventEchoIdentityDbContextStores()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }

    private static void AddAuthenticationSchemas(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = configuration.GetSection(AdventEchoIdentityJwtConfiguration.SectionName).Get<AdventEchoIdentityJwtConfiguration>();
        
        services.Configure<AdventEchoIdentityJwtConfiguration>(config =>
        {
            config.Audiences = jwtConfiguration.Required().Audiences;
            config.Issuer = jwtConfiguration.Required().Issuer;
            config.Secret = jwtConfiguration.Required().Secret;
        });
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.Required().Issuer,
                    ValidAudiences = jwtConfiguration.Required().Audiences,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Required().Secret))
                };
            });
    } 
}
