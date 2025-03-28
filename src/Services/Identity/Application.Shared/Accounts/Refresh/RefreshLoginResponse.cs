namespace AdventEcho.Identity.Application.Shared.Accounts.Refresh;

public class RefreshLoginResponse
{
    public required JwtToken AccessToken { get; init; }
    public required JwtToken RefreshToken { get; init; }
}