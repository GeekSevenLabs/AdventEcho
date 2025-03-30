namespace AdventEcho.Identity.Application.Accounts.Refresh;

public class RefreshLoginRequest
{
    public required Guid RefreshId { get; init; }
    public required bool UseCookie { get; init; }
}