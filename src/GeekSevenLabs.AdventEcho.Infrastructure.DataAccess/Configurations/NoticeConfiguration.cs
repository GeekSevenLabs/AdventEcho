using GeekSevenLabs.AdventEcho.Domain.Churches;
using GeekSevenLabs.AdventEcho.Domain.Districts;
using GeekSevenLabs.AdventEcho.Domain.Noticies;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Configurations;

internal class NoticeConfiguration : IEntityTypeConfiguration<Notice>
{
    public void Configure(EntityTypeBuilder<Notice> builder)
    {
        builder.HasKey(notice => notice.Id);
        builder.Property(notice => notice.Id).ValueGeneratedOnAdd();
        
        builder
            .Property(notice => notice.Title)
            .IsRequired()
            .HasMaxLength(150);
        
        builder
            .Property(notice => notice.Description)
            .IsRequired()
            .HasMaxLength(500);
        
        builder
            .Property(notice => notice.NotifyEveryDay)
            .IsRequired();

        builder.ComplexProperty(district => district.Period, periodBuilder =>
        {
            periodBuilder
                .Property(period => period.StartIn)
                .IsRequired();
            
            periodBuilder
                .Property(period => period.EndIn)
                .IsRequired();
        });
        
        builder
            .Property(notice => notice.DistrictId)
            .IsRequired(false);
        
        builder
            .Property(notice => notice.ChurchId)
            .IsRequired(false);
        
        builder
            .HasOne<District>()
            .WithMany()
            .HasForeignKey(notice => notice.DistrictId);
        
        builder
            .HasOne<Church>()
            .WithMany()
            .HasForeignKey(notice => notice.ChurchId);
            
    }
}