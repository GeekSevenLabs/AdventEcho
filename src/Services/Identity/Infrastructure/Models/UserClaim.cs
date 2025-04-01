using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Models;

public sealed class UserClaim : IdentityUserClaim<Guid>
{
    public AdventEchoUser User { get; private set; } = null!;
}