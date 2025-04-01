using AdventEcho.Identity.Application.Accounts.ConfirmEmail;
using AdventEcho.Identity.Application.Accounts.Login;
using AdventEcho.Identity.Application.Accounts.Refresh;
using AdventEcho.Identity.Application.Accounts.Register;
using AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;
using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Identity.Application.Shared.Accounts.Register;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Identity.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddAdventEchoIdentityHandlersServices(this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<IRequestHandler<RegisterAccountRequest, Result>, RegisterAccountHandler>();
        services.AddScoped<IRequestHandler<ConfirmEmailAccountRequest, Result>, ConfirmEmailAccountHandler>();
        services.AddScoped<IRequestHandler<LoginAccountRequest, Result<LoginAccountResponse>>, LoginAccountHandler>();
        services.AddScoped<IRequestHandler<RefreshLoginAccountRequest, Result<RefreshLoginAccountResponse>>, RefreshLoginAccountHandler>();
        
        // Validators
        services.AddScoped<IValidator<RegisterAccountRequest>, RegisterAccountValidator>();
        services.AddScoped<IValidator<ConfirmEmailAccountRequest>, ConfirmEmailAccountValidator>();
        services.AddScoped<IValidator<LoginAccountRequest>, LoginAccountValidator>();
        services.AddScoped<IValidator<RefreshLoginAccountRequest>, RefreshLoginAccountValidator>();

        return services;
    }
}