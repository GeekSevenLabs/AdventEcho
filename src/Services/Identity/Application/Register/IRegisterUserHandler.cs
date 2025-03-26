using AdventEcho.Identity.Application.Shared.Register;

namespace AdventEcho.Identity.Application.Register;

public interface IRegisterUserHandler
{
    Task<Result> HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
}