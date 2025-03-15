using FluentValidation;
using GeekSevenLabs.AdventEcho.Application.Districts.Create;
using GeekSevenLabs.AdventEcho.Application.Districts.Get;
using GeekSevenLabs.AdventEcho.Application.Districts.Update;
using GeekSevenLabs.AdventEcho.Application.Shared.Districts;
using GeekSevenLabs.AdventEcho.Application.Shared.Districts.Editor;
using GeekSevenLabs.AdventEcho.Domain.Shared;
using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Extensions;
using GeekSevenLabs.AdventEcho.Infrastructure.Identity.Extensions;
using GeekSevenLabs.AdventEcho.Kernel.Communication.Mediator;
using GeekSevenLabs.AdventEcho.Kernel;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekSevenLabs.AdventEcho.IoC.Extensions;

public static class DependencyRegistration
{
    public static void AddAdventEchoServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Mediator
        services.AddTransient<IMediatorHandler, MediatorHandler>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyMarker.Assembly);
        });
        
        // Notifications
        
        // Events
        
        // 3 Handlers
        
        // 3.1 Commands
        services.AddScoped<IRequestHandler<CreateDistrictCommand, Result<DistrictId>>, CreateDistrictCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateDistrictCommand, Result>, UpdateDistrictCommandHandler>();
        
        // 3.2 Query
        services.AddScoped<IRequestHandler<GetDistrictQuery, Result<DistrictDto>>, GetDistrictQueryHandler>();
        
        // Validators
        services.AddScoped<IValidator<EditorDistrictRequest>, EditorDistrictValidator>();

        // Services
        services.AddAdventEchoInfrastructureServices(configuration);
        services.AddAdventEchoIdentityInfrastructureServices(configuration);
    }
}