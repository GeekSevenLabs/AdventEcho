namespace AdventEcho.Identity.Application.Shared.Login;

public class LoginResponse
{
    public required JwtToken AccessToken { get; init; }
    public required JwtToken RefreshToken { get; init; }
    public required Guid UserId { get; init; }
}