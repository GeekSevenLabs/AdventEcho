namespace AdventEcho.Identity.Application.Shared.Accounts;

public class AccessToken
{
    public required string Value { get; init; }
    public required DateTime Expires { get; init; }
}