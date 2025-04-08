namespace AdventEcho.Identity.Application.Shared.Accounts.Login;

public class LoginAccountResponse
{
    public required BearerAccessToken AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}