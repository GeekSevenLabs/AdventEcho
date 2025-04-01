using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Models;

public sealed class Role : IdentityRole<Guid>
{
    public ICollection<UserRole> UserRoles { get; private set; } = [];
    public ICollection<RoleClaim> RoleClaims { get; private set; } = [];
}