namespace AdventEcho.Identity.Application.Shared.Accounts.Refresh;

public class RefreshLoginResponse
{
    public required AccessToken AccessToken { get; init; }
    public required Guid RefreshId { get; init; }
}