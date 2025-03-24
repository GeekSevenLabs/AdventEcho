namespace AdventEcho.Identity.Application.Shared.ConfirmEmail;

public class ConfirmUserEmailRequest
{
    public required Guid UserId { get; init; }
    public required string Code { get; init; }
}