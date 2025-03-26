using AdventEcho.Identity.Application.Register;
using AdventEcho.Identity.Application.Shared.Register;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Kernel.Server.Validations;

namespace AdventEcho.Presentation.Identity.Endpoints;

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
            .AddCommonProduces();
        
        return endpoints;
    }
    
    private static async Task<IResult> RegisterUserAsync(RegisterUserRequest request, IRegisterUserHandler handler)
    {
        return await handler.HandleAsync(request).ProcessResult();
    }
}