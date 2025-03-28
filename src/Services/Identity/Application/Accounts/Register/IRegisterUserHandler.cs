using AdventEcho.Identity.Application.Shared.Accounts.Register;

namespace AdventEcho.Identity.Application.Accounts.Register;

public interface IRegisterUserHandler
{
    Task<Result> HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
}