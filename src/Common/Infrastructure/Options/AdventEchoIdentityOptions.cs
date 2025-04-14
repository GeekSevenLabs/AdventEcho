// ReSharper disable once CheckNamespace
namespace AdventEcho;

public class AdventEchoIdentityOptions
{
    public const string SectionName = "AdventEchoIdentity";
    
    public AdventEchoIdentityBearerOptions Bearer { get; init; } = AdventEchoIdentityBearerOptions.Default;
    public AdventEchoIdentityCookieOptions Cookie { get; init; } = AdventEchoIdentityCookieOptions.Default;
    public AdventEchoIdentityDomainsOptions Domains { get; init; } = AdventEchoIdentityDomainsOptions.Default;
}