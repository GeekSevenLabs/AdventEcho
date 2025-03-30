namespace AdventEcho.Identity.Application.Shared.Accounts.Login;

public class LoginResponse
{
    public required AccessToken AccessToken { get; init; }
    public required Guid RefreshId { get; init; }
}