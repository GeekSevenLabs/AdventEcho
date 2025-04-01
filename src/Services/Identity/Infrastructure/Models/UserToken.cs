using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Models;

public sealed class UserToken : IdentityUserToken<Guid>
{
    public AdventEchoUser User { get; private set; } = null!;
}