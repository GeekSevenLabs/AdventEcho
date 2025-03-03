using GeekSevenLabs.AdventEcho.Domain.Churches;
using GeekSevenLabs.AdventEcho.Domain.Districts;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Configurations;

internal class ChurchConfiguration : IEntityTypeConfiguration<Church>
{
    public void Configure(EntityTypeBuilder<Church> builder)
    {
        builder.HasKey(church => church.Id);
        builder.Property(church => church.Id).ValueGeneratedOnAdd();
        
        builder
            .Property(church => church.Name)
            .IsRequired()
            .HasMaxLength(150);
        
        builder
            .Property(church => church.DistrictId)
            .IsRequired();
        
        builder
            .HasOne<District>()
            .WithMany()
            .HasForeignKey(church => church.DistrictId);
    }
}