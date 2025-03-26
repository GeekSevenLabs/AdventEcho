using AdventEcho.Identity.Application.Shared.Login;

namespace AdventEcho.Identity.Application.Login;

public interface IUserLoginHandler
{
    Task<Result<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default);
}