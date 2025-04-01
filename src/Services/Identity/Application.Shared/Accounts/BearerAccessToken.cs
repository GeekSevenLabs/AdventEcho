namespace AdventEcho.Identity.Application.Shared.Accounts;

public class BearerAccessToken
{
    public required string Value { get; init; }
    public required DateTimeOffset Expiration { get; init; }
}