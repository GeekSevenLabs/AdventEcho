using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Models;

public sealed class UserRole : IdentityUserRole<Guid>
{
    public AdventEchoUser User { get; private set; } = null!;
    public Role Role { get; private set; } = null!;
}