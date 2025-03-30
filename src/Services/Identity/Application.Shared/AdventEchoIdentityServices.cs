using AdventEcho.Identity.Application.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Identity.Application.Shared;

public static class AdventEchoIdentityServices
{
    public static void AddAdventEchoIdentityForClient(this IServiceCollection services)
    {
        services.AddScoped<IAccountViewService, AccountViewService>();
    }
}