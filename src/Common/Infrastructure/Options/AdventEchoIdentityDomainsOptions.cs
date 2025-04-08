// ReSharper disable once CheckNamespace
namespace AdventEcho;

public class AdventEchoIdentityDomainsOptions 
{
    public static AdventEchoIdentityDomainsOptions Default => new()
    {
        Identity = string.Empty,
        Web = string.Empty,
        Api = string.Empty,
    };

    public required string Identity { get; init; }
    public required string Web { get; init; }
    public required string Api { get; init; }
}