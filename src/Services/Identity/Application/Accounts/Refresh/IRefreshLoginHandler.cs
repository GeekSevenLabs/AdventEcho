using AdventEcho.Identity.Application.Shared.Accounts.Refresh;

namespace AdventEcho.Identity.Application.Accounts.Refresh;

public interface IRefreshLoginHandler
{
    Task<Result<RefreshLoginResponse>> HandleAsync(RefreshLoginRequest request, CancellationToken cancellationToken = default);
}