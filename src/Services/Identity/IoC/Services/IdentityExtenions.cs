using AdventEcho.Identity.Infrastructure.Extensions;
using AdventEcho.Identity.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Identity.IoC.Services;

public static class IdentityExtensions
{
    public static void AddCustomIdentity(this IServiceCollection services)
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
}