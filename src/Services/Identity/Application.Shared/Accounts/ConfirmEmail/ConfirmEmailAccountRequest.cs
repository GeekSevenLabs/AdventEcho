using AdventEcho.Kernel.Application.Shared.Messages;

namespace AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;

public class ConfirmEmailAccountRequest : ICommand
{
    public required Guid UserId { get; init; }
    public required string Code { get; init; }
}