// ReSharper disable once CheckNamespace
namespace AdventEcho;

public class BearerAccessToken
{
    public required string Value { get; init; }
    public required DateTimeOffset Expiration { get; init; }
}