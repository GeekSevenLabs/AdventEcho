using AdventEcho.Identity.Application;
using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Models;

public sealed class User : IdentityUser<Guid>, IUser
{
    public User(string nome)
    {
        Name = nome;
    }

    private User() { }
    
    public ICollection<UserClaim> Claims { get; private set; } = [];
    public ICollection<UserLogin> Logins { get; private set; } = [];
    public ICollection<UserToken> Tokens { get; private set; } = [];
    public ICollection<UserRole> UserRoles { get; private set; } = [];
    public string? Name { get; private set; }
}

public sealed class Role : IdentityRole<Guid>
{
    public ICollection<UserRole> UserRoles { get; private set; } = [];
    public ICollection<RoleClaim> RoleClaims { get; private set; } = [];
}

public sealed class UserRole : IdentityUserRole<Guid>
{
    public User User { get; private set; } = null!;
    public Role Role { get; private set; } = null!;
}

public sealed class UserClaim : IdentityUserClaim<Guid>
{
    public User User { get; private set; } = null!;
}

public sealed class UserLogin : IdentityUserLogin<Guid>
{
    public User User { get; private set; } = null!;
}

public sealed class RoleClaim : IdentityRoleClaim<Guid>
{
    public Role Role { get; private set; } = null!;
}

public sealed class UserToken : IdentityUserToken<Guid>
{
    public User User { get; private set; } = null!;
}