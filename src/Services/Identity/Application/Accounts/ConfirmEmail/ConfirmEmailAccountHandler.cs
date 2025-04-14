using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;

namespace AdventEcho.Identity.Application.Accounts.ConfirmEmail;

public class ConfirmEmailAccountHandler(IUserService service) : ICommandHandler<ConfirmEmailAccountRequest>
{
    public async Task<Result> Handle(ConfirmEmailAccountRequest request, CancellationToken cancellationToken)
    {
        return await service.ConfirmEmailAsync(request.UserId, request.Code, cancellationToken);
    }
}