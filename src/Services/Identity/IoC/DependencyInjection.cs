using System.Text;
using AdventEcho.Identity.Application.Extensions;
using AdventEcho.Identity.Infrastructure.Extensions;
using AdventEcho.Identity.Infrastructure.Models;
using AdventEcho.Identity.Infrastructure.Options;
using AdventEcho.Kernel.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Resend;

namespace AdventEcho.Identity.IoC;

public static class DependencyInjection
{
    public static void AddAdventEchoIdentity(this WebApplicationBuilder builder)
    {
        // Configurations
        builder.AddConfigurations();
        
        // Infrastructure
        builder.AddAdventEchoIdentityInfrastructure();
        
        // Applications
        builder.Services.AddAdventEchoIdentityApplicationServices();

        // Resend
        builder.Services.AddResend();
        
        // Identity
        builder.Services.AddIdentity();
        
        // Authentication and Authorization
        builder.Services.AddAuthenticationSchemas(builder.Configuration);
        builder.Services.AddAuthorization();
    }

    private static void AddConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions();

        var domainConfiguration = builder.Configuration.GetSection(AdventEchoIdentityDomainsConfiguration.SectionName).Get<AdventEchoIdentityDomainsConfiguration>().Required();
        
        builder.Services.Configure<AdventEchoIdentityDomainsConfiguration>(options =>
        {
            options.ApiIdentity = domainConfiguration.ApiIdentity;
        });
        
        builder.Services.Configure<ResendClientOptions>(o =>
        {
            o.ApiToken = builder.Configuration["ResendKey"]!;
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
