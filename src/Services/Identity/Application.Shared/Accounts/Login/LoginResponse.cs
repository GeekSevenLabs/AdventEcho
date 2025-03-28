namespace AdventEcho.Identity.Application.Shared.Accounts.Login;

public class LoginResponse
{
    public required JwtToken AccessToken { get; init; }
    public required JwtToken RefreshToken { get; init; }
}