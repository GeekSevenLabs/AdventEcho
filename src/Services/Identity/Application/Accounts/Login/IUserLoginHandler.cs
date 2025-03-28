using AdventEcho.Identity.Application.Shared.Accounts.Login;

namespace AdventEcho.Identity.Application.Accounts.Login;

public interface IUserLoginHandler
{
    Task<Result<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default);
}