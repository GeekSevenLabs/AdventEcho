using AdventEcho.Identity.Application.Accounts.ConfirmEmail;
using AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Kernel.Server.Validations;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

public class ConfirmUserEmailEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/confirm-email", ConfirmUserLoginAsync)
            .UseValidationFor<ConfirmUserEmailRequest>()
            .WithName("ConfirmUserEmail")
            .WithSummary("Confirm user email")
            .WithDescription("Confirm user e-mail by providing the token received in the e-mail.")
            .AddCommonProduces()
            .AllowAnonymous();
        
        return endpoints;
    }

    private static async Task<IResult> ConfirmUserLoginAsync(ConfirmUserEmailRequest request, IConfirmUserEmailHandler handler)
    {
        return await handler.HandleAsync(request).ProcessResult();
    }
}