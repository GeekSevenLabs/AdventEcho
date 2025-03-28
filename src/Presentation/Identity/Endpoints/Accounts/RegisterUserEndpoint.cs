using AdventEcho.Identity.Application.Accounts.Register;
using AdventEcho.Identity.Application.Shared.Accounts.Register;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

public abstract class RegisterUserEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/", RegisterUserAsync)
            .UseValidationFor<RegisterUserRequest>()
            .WithName("RegisterUser")
            .WithSummary("Register a new user.")
            .WithDescription("Register a new user with the provided information. After registration, the user will receive an email to confirm the registration.")
            .AddCommonProduces()
            .AllowAnonymous();
        
        return endpoints;
    }
    
    private static async Task<IResult> RegisterUserAsync(RegisterUserRequest request, IRegisterUserHandler handler)
    {
        return await handler.HandleAsync(request).ProcessResult();
    }
}