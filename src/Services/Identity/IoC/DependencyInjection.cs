using System.Text;
using AdventEcho.Identity.Application;
using AdventEcho.Identity.Application.Shared;
using AdventEcho.Identity.Infrastructure.Extensions;
using AdventEcho.Identity.Infrastructure.Models;
using AdventEcho.Identity.Infrastructure.Options;
using AdventEcho.Kernel.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Identity.IoC;

public static class DependencyInjection
{
    public static void AddAdventEchoIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        // Configurations
        services.AddConfigurations(configuration);
        
        // Db Context
        services.AddAdventEchoIdentityInfrastructure(configuration);

        // Identity
        services.AddIdentity();
        
        // Authentication and Authorization
        services.AddAuthenticationSchemas(configuration);
        services.AddAuthorization();
        
        // Services
        services.AddTransient<IEmailSender<User>, EmailSenderFake>();
        services.AddScoped<IAccountService, AccountService>();
    }

    private static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        var domainConfiguration = configuration.GetSection(AdventEchoIdentityDomainsConfiguration.SectionName).Get<AdventEchoIdentityDomainsConfiguration>().Required();
        
        services.Configure<AdventEchoIdentityDomainsConfiguration>(options =>
        {
            options.ApiIdentity = domainConfiguration.ApiIdentity;
        });
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
            .AddAdventEchoStores()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }

    private static void AddAuthenticationSchemas(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = configuration.GetSection(AdventEchoIdentityJwtConfiguration.SectionName).Get<AdventEchoIdentityJwtConfiguration>();

        services.AddOptions();
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

//TODO: Remove this class and implement the real email sender
file class EmailSenderFake : IEmailSender<User>
{
    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        Console.WriteLine($"EMAIL CONFIRMATION ::: {email}:{confirmationLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        return Task.CompletedTask;
    }
}