using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Models;

public sealed class UserLogin : IdentityUserLogin<Guid>
{
    public AdventEchoUser User { get; private set; } = null!;
}