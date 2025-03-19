namespace AdventEcho.Identity.Infrastructure.Configurations;

internal class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        // Primary key
        builder.HasKey(uc => uc.Id);

        // Maps to the AspNetUserClaims table
        builder.ToTable("UserClaims");

        builder.Property(uc => uc.UserId).IsRequired();
        
        builder.Property(uc => uc.ClaimType).HasMaxLength(256);
        builder.Property(uc => uc.ClaimValue).HasMaxLength(256);
        
        builder
            .HasOne(uc => uc.User)
            .WithMany(user => user.Claims)
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();
        
    }
}