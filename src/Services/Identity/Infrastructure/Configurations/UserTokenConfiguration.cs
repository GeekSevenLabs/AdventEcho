namespace AdventEcho.Identity.Infrastructure.Configurations;

internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        // Composite primary key consisting of the UserId, LoginProvider and Name
        builder.HasKey(userToken => new { userToken.UserId, userToken.LoginProvider, userToken.Name });

        // Limit the size of the composite key columns due to common DB restrictions
        builder.Property(userToken => userToken.LoginProvider).HasMaxLength(AdventEchoIdentityOption.Db.MaxKeyLength);
        builder.Property(userToken => userToken.Name).HasMaxLength(AdventEchoIdentityOption.Db.MaxKeyLength);

        // Maps to the AspNetUserTokens table
        builder.ToTable("UserTokens");
        
        builder.HasOne(userToken => userToken.User).WithMany(user => user.Tokens).HasForeignKey(userToken => userToken.UserId);
    }
}