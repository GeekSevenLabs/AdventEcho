using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;
using AdventEcho.Kernel.Application.Handlers;
using AdventEcho.Kernel.Application.Shared.Messages.Results;

namespace AdventEcho.Identity.Application.Accounts.ConfirmEmail;

public class ConfirmEmailAccountHandler(IUserService service) : ICommandHandler<ConfirmEmailAccountRequest>
{
    public async Task<Result> Handle(ConfirmEmailAccountRequest request, CancellationToken cancellationToken)
    {
        return await service.ConfirmEmailAsync(request.UserId, request.Code, cancellationToken);
    }
}