using AdventEcho.Identity.Application.Shared.Accounts.Register;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Kernel.Application.Shared.Messages.Results;

namespace AdventEcho.Identity.Application.Shared.Services;

public interface IAccountViewService
{
    Task<Result> RegisterAsync(RegisterAccountRequest request, CancellationToken cancellationToken);
}