using AdventEcho.Identity.Infrastructure.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdventEcho.Identity.Infrastructure.Extensions;

public static class HostExtensions
{
    public static void CreateDataBaseAndApplyMigrations(this IHost host)
    {
        var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        try
        {
            var db = services.GetRequiredService<AdventEchoIdentityDbContext>();
            db.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<AdventEchoIdentityDbContext>>();
            logger.LogError(ex, "An error occurred creating the DB for AdventEchoIdentityDbContext.");
        }
        
    }
}