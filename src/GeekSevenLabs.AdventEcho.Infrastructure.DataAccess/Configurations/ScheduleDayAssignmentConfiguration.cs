using GeekSevenLabs.AdventEcho.Domain.Schedules;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Configurations;

internal class ScheduleDayAssignmentConfiguration : IEntityTypeConfiguration<ScheduleDayAssignment>
{
    public void Configure(EntityTypeBuilder<ScheduleDayAssignment> builder)
    {
        builder.HasKey(assignment => assignment.Id);
        builder.Property(assignment => assignment.Id).ValueGeneratedOnAdd();
        
        builder
            .Property(assignment => assignment.Name)
            .IsRequired()
            .HasMaxLength(150);
        
        builder
            .Property(assignment => assignment.ForScheduleType)
            .IsRequired();
    }
}