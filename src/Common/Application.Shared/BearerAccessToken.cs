namespace AdventEcho.Kernel.Application.Shared;

public class BearerAccessToken
{
    public required string Value { get; init; }
    public required DateTimeOffset Expiration { get; init; }
}