using AdventEcho.Kernel.Application.Shared;

namespace AdventEcho.Identity.Application.Shared.Accounts.Refresh;

public class RefreshLoginAccountResponse
{
    public required BearerAccessToken AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}