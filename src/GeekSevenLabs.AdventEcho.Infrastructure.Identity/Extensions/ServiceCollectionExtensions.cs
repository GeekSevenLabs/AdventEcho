using GeekSevenLabs.AdventEcho.Domain;
using GeekSevenLabs.AdventEcho.Infrastructure.Identity.Contexts;
using GeekSevenLabs.AdventEcho.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;

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
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddSingleton<IEmailSender<ApplicationUser>, AdventEchoEmailSender>();
        
        services.Configure<AuthMessageSenderOptions>(o =>
        {
            o.SendGridKey = configuration["SendGridKey"];
        });

        services.AddOptions();
        services.AddHttpClient<ResendClient>();
        services.Configure<ResendClientOptions>(o =>
        {
            o.ApiToken = configuration["ResendKey"]!;
        });
        services.AddTransient<IResend, ResendClient>();
    }
}