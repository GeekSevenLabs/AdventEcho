using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Contexts;
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
        // services.AddScoped<IRawMaterialRepository, RawMaterialRepository>();
        // services.AddScoped<IProductRepository, ProductRepository>();
        // services.AddScoped<ICustomerRepository, CustomerRepository>();
        // services.AddScoped<IQuotationRepository, QuotationRepository>();
        
        // Queries
        // services.AddScoped<IRawMaterialQueries, RawMaterialQueries>();
        // services.AddScoped<IProductQueries, ProductQueries>();
        // services.AddScoped<ICustomerQueries, CustomerQueries>();
        // services.AddScoped<IQuotationQueries, QuotationQueries>();
        
        // Services
        // services.AddScoped<IPricingService, PricingService>();
    }
}