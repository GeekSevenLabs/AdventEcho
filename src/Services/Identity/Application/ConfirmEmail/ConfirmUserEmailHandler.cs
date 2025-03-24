using AdventEcho.Identity.Application.Shared.ConfirmEmail;
using AdventEcho.Identity.Domain.Users.Services;

namespace AdventEcho.Identity.Application.ConfirmEmail;

internal class ConfirmUserEmailHandler(IUserService userService) : IConfirmUserEmailHandler
{
    public async Task<Result> HandleAsync(ConfirmUserEmailRequest request, CancellationToken cancellationToken = default)
    {
        return await userService.ConfirmEmailAsync(request.UserId, request.Code, cancellationToken);
    }
}