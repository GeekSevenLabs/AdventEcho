using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Models;

public sealed class RoleClaim : IdentityRoleClaim<Guid>
{
    public Role Role { get; private set; } = null!;
}