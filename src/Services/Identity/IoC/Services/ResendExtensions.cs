using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace AdventEcho.Identity.IoC.Services;

public static class ResendExtensions
{
    public static void AddResend(this IServiceCollection services)
    {
        services.AddHttpClient<ResendClient>();
        services.AddTransient<IResend, ResendClient>();
    }
}