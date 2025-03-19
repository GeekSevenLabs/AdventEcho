using AdventEcho.Identity.Application;
using AdventEcho.Identity.Infrastructure.Contexts;
using AdventEcho.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Identity.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAdventEchoIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<AdventEchoIdentityDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(AdventEchoIdentityConfiguration.ConnectionStringName));
        });

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserManager, UserManagerInternal>();
    }

    public static IdentityBuilder AddAdventEchoStores(this IdentityBuilder builder)
    {
        return builder.AddEntityFrameworkStores<AdventEchoIdentityDbContext>();
    }
}