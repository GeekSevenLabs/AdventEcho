using AdventEcho.Identity.Application.Shared.ConfirmEmail;

namespace AdventEcho.Identity.Application.ConfirmEmail;

public interface IConfirmUserEmailHandler
{
    Task<Result> HandleAsync(ConfirmUserEmailRequest request, CancellationToken cancellationToken = default);
}