using GeekSevenLabs.AdventEcho.Application.Districts;
using GeekSevenLabs.AdventEcho.Domain.Districts;
using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Contexts;
using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Queries;
using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Repositories;
using GeekSevenLabs.AdventEcho.Kernel.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAdventEchoInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<AdventEchoDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AdventEchoConnection"));
        });

        // Suporte services
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AdventEchoDbContext>());
        
        // Repositories
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        
        // Queries
        services.AddScoped<IDistrictQueries, DistrictQueries>();
        
        // Services
        // services.AddScoped<IPricingService, PricingService>();
    }
}