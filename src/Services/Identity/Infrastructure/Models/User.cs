using AdventEcho.Identity.Domain.Users;
using Cblx.Blocks;
using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Models;

[HasPrivateEmptyConstructor]
public sealed partial class AdventEchoUser : IdentityUser<Guid>
{
    public AdventEchoUser(NameVo nome)
    {
        Name = nome;
    }
    
    public ICollection<UserClaim> Claims { get; private set; } = [];
    public ICollection<UserLogin> Logins { get; private set; } = [];
    public ICollection<UserToken> Tokens { get; private set; } = [];
    public ICollection<UserRole> UserRoles { get; private set; } = [];

    public NameVo Name { get; private set; } = NameVo.Empty;
}