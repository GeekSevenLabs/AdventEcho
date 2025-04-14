namespace AdventEcho.Identity.Application.Shared.Accounts.Refresh;

public class RefreshLoginAccountRequest : ICommand<RefreshLoginAccountResponse>
{
    public required string RefreshToken { get; init; }
    public bool UseCookie { get; init; }
}