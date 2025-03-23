using AdventEcho.Identity.Application.Tokens;
using AdventEcho.Identity.Domain.Users.Services;
using AdventEcho.Identity.Infrastructure.Contexts;
using AdventEcho.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        services.AddScoped<IUserService, UserService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddSingleton<IEmailSender<User>, IdentityEmailSender>();
    }

    public static IdentityBuilder AddAdventEchoIdentityDbContextStores(this IdentityBuilder builder)
    {
        return builder.AddEntityFrameworkStores<AdventEchoIdentityDbContext>();
    }
}