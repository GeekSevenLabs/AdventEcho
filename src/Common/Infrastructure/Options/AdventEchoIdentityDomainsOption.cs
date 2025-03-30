namespace AdventEcho.Kernel.Infrastructure.Options;

public class AdventEchoIdentityDomainsOption
{
    public static AdventEchoIdentityDomainsOption Default => new()
    {
        Identity = string.Empty,
        Web = string.Empty
    };
    
    public required string Identity { get; init; }
    public required string Web { get; init; }
}