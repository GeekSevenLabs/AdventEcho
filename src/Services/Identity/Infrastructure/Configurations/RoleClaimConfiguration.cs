namespace AdventEcho.Identity.Infrastructure.Configurations;

internal class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        // Primary key
        builder.HasKey(rc => rc.Id);
        
        // Maps to the AspNetRoles table
        builder.Property(rc => rc.RoleId).IsRequired();
        builder.Property(rc => rc.ClaimType).IsRequired().HasMaxLength(300);
        builder.Property(rc => rc.ClaimValue).IsRequired().HasMaxLength(300);

        // Maps to the AspNetRoleClaims table
        builder.ToTable("RoleClaims");
        
        // Foreign key to the AspNetRoles table
        builder.HasOne(rc => rc.Role).WithMany(r => r.RoleClaims).HasForeignKey(rc => rc.RoleId).IsRequired();
    }
}