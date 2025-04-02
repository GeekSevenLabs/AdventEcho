using AdventEcho.Identity.Application.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Identity.Application.Shared;

public static class AdventEchoIdentityApplicationServices
{
    public static IServiceCollection AddAdventEchoIdentityApplicationSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountViewService, AccountViewService>();

        return services;
    }
}