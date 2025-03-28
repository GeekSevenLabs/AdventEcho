using AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;

namespace AdventEcho.Identity.Application.Accounts.ConfirmEmail;

public interface IConfirmUserEmailHandler
{
    Task<Result> HandleAsync(ConfirmUserEmailRequest request, CancellationToken cancellationToken = default);
}