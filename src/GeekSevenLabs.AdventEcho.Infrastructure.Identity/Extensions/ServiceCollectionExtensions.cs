using GeekSevenLabs.AdventEcho.Domain;
using GeekSevenLabs.AdventEcho.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekSevenLabs.AdventEcho.Infrastructure.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAdventEchoIdentityInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<AdventEchoIdentityDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AdventEchoIdentityConnection"));
        });
        
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