namespace AdventEcho.Identity.Infrastructure.Configurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        // Primary key
        builder.HasKey(r => new { r.UserId, r.RoleId });

        // Maps to the AspNetUserRoles table
        builder.ToTable("UserRoles");
        
        // Foreign key to the AspNetUsers table
        builder.HasOne(r => r.User).WithMany(u => u.UserRoles).HasForeignKey(r => r.UserId).IsRequired();
        builder.HasOne(r => r.Role).WithMany(r => r.UserRoles).HasForeignKey(r => r.RoleId).IsRequired();
    }
}