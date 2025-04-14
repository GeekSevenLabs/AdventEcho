using AdventEcho.Identity.Application.Services.Caches;
using AdventEcho.Identity.Application.Services.Cookies;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Domain;
using AdventEcho.Identity.Domain.Users;
using AdventEcho.Identity.Infrastructure.Repositories;
using AdventEcho.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;
using Resend;
using StackExchange.Redis;

namespace AdventEcho.Identity.Infrastructure;

public static class InfrastructureServices
{
    private const string AdventEchoIdentityConnectionString = "AdventEchoIdentityConnection";
    private const string AdventEchoIdentityRedisConnectionString = "AdventEchoIdentityRedisConnection";
    
    public static WebApplicationBuilder AddAdventEchoIdentityInfrastructureServices(this WebApplicationBuilder builder)
    {
        // Domain Services
        builder.Services.AddDbContextFactory<AdventEchoIdentityDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(AdventEchoIdentityConnectionString));
        });
        
        builder.Services.AddScoped<IAdventEchoIdentityUnitOfWork>(provider => provider
            .GetRequiredService<IDbContextFactory<AdventEchoIdentityDbContext>>()
            .CreateDbContext()
        );

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        
        // Infrastructure Services
        
        builder.Services.AddDbContextFactory<CustomIdentityDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(AdventEchoIdentityConnectionString));
        });

        builder.Services.AddScoped<IBearerTokenService, BearerTokenService>();
        builder.Services.AddScoped<ICacheService, CacheService>();
        builder.Services.AddScoped<ICookieService, CookieService>();
        builder.Services.AddScoped<IUserService, UserService>();
        
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.AddTransient<IEmailSender<AdventEchoUser>, IdentityEmailSender>();
        
        builder.Services.AddHttpClient<ResendClient>();
        builder.Services.AddTransient<IResend, ResendClient>();
        
        var redisConnection = builder.Configuration.GetConnectionString(AdventEchoIdentityRedisConnectionString);
        builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection!));
        
        //__
        return builder;
    }
    
    public static IdentityBuilder AddAdventEchoIdentityDbContextStores(this IdentityBuilder builder)
    {
        return builder.AddEntityFrameworkStores<CustomIdentityDbContext>();
    }
    
    public static async Task ApplyMigrationsAsync(this IHost builder, CancellationToken cancellationToken = default)
    {
        await using var scope = builder.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var contextFactory = services.GetRequiredService<IDbContextFactory<CustomIdentityDbContext>>();
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        await RunMigrationAsync(context, cancellationToken);
    }
    
    private static async Task RunMigrationAsync(CustomIdentityDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(() => dbContext.Database.MigrateAsync(cancellationToken));
    }
}