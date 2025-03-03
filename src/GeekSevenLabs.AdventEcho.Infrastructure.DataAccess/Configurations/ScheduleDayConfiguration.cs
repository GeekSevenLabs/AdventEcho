using GeekSevenLabs.AdventEcho.Domain.People;
using GeekSevenLabs.AdventEcho.Domain.Schedules;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Configurations;

internal class ScheduleDayConfiguration : IEntityTypeConfiguration<ScheduleDay>
{
    public void Configure(EntityTypeBuilder<ScheduleDay> builder)
    {
        builder.HasKey(day => day.Id);
        builder.Property(day => day.Id).ValueGeneratedOnAdd();
        
        builder.Property(day => day.Day).IsRequired();
        builder.Property(day => day.ScheduleId).IsRequired();
        builder.Property(day => day.EventId).IsRequired(false);
        
        builder
            .HasOne<Schedule>()
            .WithMany(schedule => schedule.Days)
            .HasForeignKey(day => day.ScheduleId);
        
        builder
            .HasOne<ScheduleEvent>()
            .WithMany()
            .HasForeignKey(day => day.EventId);
        
        builder
            .HasMany(day => day.People)
            .WithMany(person => person.ScheduleDays)
            .UsingEntity<ScheduledPerson>(
                right => right.HasOne<Person>().WithMany(day => day.ScheduledPeople),
                left => left.HasOne<ScheduleDay>().WithMany(person => person.ScheduledPeople)
            );
    }
}