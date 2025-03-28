using AdventEcho.Identity.Application.Accounts.ConfirmEmail;
using AdventEcho.Identity.Application.Accounts.Login;
using AdventEcho.Identity.Application.Accounts.Refresh;
using AdventEcho.Identity.Application.Accounts.Register;
using AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;
using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Identity.Application.Shared.Accounts.Register;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Identity.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAdventEchoIdentityApplicationServices(this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
        services.AddSingleton<IValidator<RegisterUserRequest>, RegisterUserValidator>();

        services.AddScoped<IConfirmUserEmailHandler, ConfirmUserEmailHandler>();
        services.AddSingleton<IValidator<ConfirmUserEmailRequest>, ConfirmUserEmailValidator>();

        services.AddScoped<IUserLoginHandler, UserLoginHandler>();
        services.AddSingleton<IValidator<LoginRequest>, LoginValidator>();

        services.AddScoped<IRefreshLoginHandler, RefreshLoginHandler>();
    }
}