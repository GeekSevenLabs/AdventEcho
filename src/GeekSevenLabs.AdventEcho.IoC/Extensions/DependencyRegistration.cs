using GeekSevenLabs.AdventEcho.Kernel.Communication.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace GeekSevenLabs.AdventEcho.IoC.Extensions;

public static class DependencyRegistration
{
    public static void AddAdventEchoApplicationServices(this IServiceCollection services)
    {
        // Mediator
        services.AddTransient<IMediatorHandler, MediatorHandler>();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Application.AssemblyMarker.Assembly);
        });

    }
}