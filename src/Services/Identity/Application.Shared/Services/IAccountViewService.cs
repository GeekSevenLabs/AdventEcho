using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Identity.Application.Shared.Accounts.Register;
using AdventEcho.Kernel.Application.Shared.Messages.Results;

namespace AdventEcho.Identity.Application.Shared.Services;

public interface IAccountViewService
{
    Task<Result> RegisterAsync(RegisterAccountRequest request, CancellationToken cancellationToken);
    Task<Result<RefreshLoginAccountResponse>> RefreshLoginAsync(RefreshLoginAccountRequest request, CancellationToken cancellationToken);
    Task<Result<LoginAccountResponse>> LoginAsync(LoginAccountRequest request, CancellationToken cancellationToken);
}