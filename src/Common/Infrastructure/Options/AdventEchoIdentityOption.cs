namespace AdventEcho.Kernel.Infrastructure.Options;

public class AdventEchoIdentityForClientOption
{
    public const string SectionName = "AdventEchoIdentity";
    
    public AdventEchoIdentityDomainsOption Domains { get; set; } = AdventEchoIdentityDomainsOption.Default;
    public AdventEchoIdentityJwtForClientOption Jwt { get; set; } = AdventEchoIdentityJwtForClientOption.Default;
    public AdventEchoIdentityCookieOption Cookie { get; set; } = AdventEchoIdentityCookieOption.Default;
}