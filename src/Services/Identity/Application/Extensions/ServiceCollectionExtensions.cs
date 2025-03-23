using AdventEcho.Identity.Application.Register;
using AdventEcho.Identity.Application.Shared.Register;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Identity.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAdventEchoIdentityApplicationServices(this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
        services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();
    }
}