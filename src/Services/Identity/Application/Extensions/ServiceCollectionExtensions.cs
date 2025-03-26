using AdventEcho.Identity.Application.ConfirmEmail;
using AdventEcho.Identity.Application.Login;
using AdventEcho.Identity.Application.Register;
using AdventEcho.Identity.Application.Shared.ConfirmEmail;
using AdventEcho.Identity.Application.Shared.Login;
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

        services.AddScoped<IConfirmUserEmailHandler, ConfirmUserEmailHandler>();
        services.AddScoped<IValidator<ConfirmUserEmailRequest>, ConfirmUserEmailValidator>();

        services.AddScoped<IUserLoginHandler, UserLoginHandler>();
        services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
    }
}