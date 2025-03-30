using AdventEcho.Identity.Application.Extensions;
using AdventEcho.Identity.Infrastructure.Extensions;
using AdventEcho.Identity.IoC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Identity.IoC;

public static class DependencyInjection
{
    public static void AddAdventEchoSecurity(this WebApplicationBuilder builder)
    {
        // Configurations
        var options = builder.Services.AddAdventEchoIdentityOptions(builder.Configuration);
        
        // HttpContextAccessor
        builder.Services.AddHttpContextAccessor();
        
        // Redis
        builder.Services.AddRedisCache(builder.Configuration);
        
        // Cors
        builder.Services.AddAdventEchoCors(options);

        // Infrastructure
        builder.AddAdventEchoIdentityInfrastructure();

        // Applications
        builder.Services.AddAdventEchoIdentityApplicationServices();

        // Resend
        builder.Services.AddResend();

        // Identity
        builder.Services.AddCustomIdentity();

        // Authentication and Authorization
        builder.Services.AddAdventEchoAuthenticationSchemas(options);
        builder.Services.AddAuthorization();
    }

    public static void UseAdventEchoSecurity(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAdventEchoCors();
        app.UseAuthentication();
        app.UseAuthorization();
    }
    
}