namespace AdventEcho.Identity.Application.Shared.Accounts;

public class JwtToken
{
    public required string Value { get; init; }
    public required DateTime Expires { get; init; }
    public required DateTime ValidIn { get; init; }
}