using AdventEcho.Identity.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdventEcho.Identity.Infrastructure.Contexts;

internal class AdventEchoIdentityDbContext(DbContextOptions<AdventEchoIdentityDbContext> options) : IdentityDbContext<
    User, 
    Role,
    Guid,
    UserClaim,
    UserRole,
    UserLogin,
    RoleClaim,
    UserToken>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new UserClaimConfiguration());
        builder.ApplyConfiguration(new UserLoginConfiguration());
        builder.ApplyConfiguration(new RoleClaimConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());
    }
}