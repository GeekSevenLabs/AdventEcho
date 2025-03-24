using AdventEcho.Identity.Application.Tokens;
using AdventEcho.Identity.Domain.Users.Services;
using AdventEcho.Identity.Infrastructure.Contexts;
using AdventEcho.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventEcho.Identity.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAdventEchoIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<AdventEchoIdentityDbContext>("AdventEchoIdentityDataBase");

        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.AddSingleton<IEmailSender<User>, IdentityEmailSender>();
    }

    public static IdentityBuilder AddAdventEchoIdentityDbContextStores(this IdentityBuilder builder)
    {
        return builder.AddEntityFrameworkStores<AdventEchoIdentityDbContext>();
    }
}