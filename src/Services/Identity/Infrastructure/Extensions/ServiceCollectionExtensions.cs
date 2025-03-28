using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Domain.Users.Services;
using AdventEcho.Identity.Infrastructure.Contexts;
using AdventEcho.Identity.Infrastructure.Services;
using AdventEcho.Kernel.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventEcho.Identity.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    private const string AdventEchoIdentityConnectionString = "AdventEchoIdentityConnection";

    public static void AddAdventEchoIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContextFactory<AdventEchoIdentityDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(AdventEchoIdentityConnectionString));
        });

        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.AddSingleton<IEmailSender<User>, IdentityEmailSender>();
    }

    public static IdentityBuilder AddAdventEchoIdentityDbContextStores(this IdentityBuilder builder)
    {
        return builder.AddEntityFrameworkStores<AdventEchoIdentityDbContext>();
    }

    public static async Task ApplyMigrationsAsync(this IHost builder, CancellationToken cancellationToken = default)
    {
        await using var scope = builder.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var contextFactory = services.GetRequiredService<IDbContextFactory<AdventEchoIdentityDbContext>>();
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        await RunMigrationAsync(context, cancellationToken);
    }

    private static async Task RunMigrationAsync(AdventEchoIdentityDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(() => dbContext.Database.MigrateAsync(cancellationToken));
    }
}