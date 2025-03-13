using GeekSevenLabs.AdventEcho.Domain.Churches;
using GeekSevenLabs.AdventEcho.Domain.Districts;
using GeekSevenLabs.AdventEcho.Domain.Noticies;
using GeekSevenLabs.AdventEcho.Domain.People;
using GeekSevenLabs.AdventEcho.Domain.Schedules;
using GeekSevenLabs.AdventEcho.Domain.Shared;
using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Configurations;
using GeekSevenLabs.AdventEcho.Kernel.Data;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Contexts;

public class AdventEchoDbContext(DbContextOptions<AdventEchoDbContext> options) : DbContext(options), IUnitOfWork
{
    public required DbSet<District> Districts { get; init; }
    public required DbSet<Church> Churches { get; init; }
    public required DbSet<Notice> Notices { get; init; }
    public required DbSet<Person> People { get; init; }
    
    public required DbSet<Schedule> Schedules { get; init; }
    public required DbSet<ScheduleDay> ScheduleDays { get; init; }
    public required DbSet<ScheduleDayAssignment> ScheduleDayAssignments { get; init; }
    public required DbSet<ScheduleEvent> ScheduleEvents { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ChurchConfiguration());
        modelBuilder.ApplyConfiguration(new DistrictConfiguration());
        modelBuilder.ApplyConfiguration(new NoticeConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        
        modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleDayConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleDayAssignmentConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleEventConfiguration());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<ChurchId>().HaveConversion<ChurchId.EfCoreValueConverter>();
        configurationBuilder.Properties<DistrictId>().HaveConversion<DistrictId.EfCoreValueConverter>();
        configurationBuilder.Properties<NoticeId>().HaveConversion<NoticeId.EfCoreValueConverter>();
        configurationBuilder.Properties<PersonId>().HaveConversion<PersonId.EfCoreValueConverter>();
        configurationBuilder.Properties<ScheduleDayAssignmentId>().HaveConversion<ScheduleDayAssignmentId.EfCoreValueConverter>();
        configurationBuilder.Properties<ScheduleDayId>().HaveConversion<ScheduleDayId.EfCoreValueConverter>();
        configurationBuilder.Properties<ScheduleEventId>().HaveConversion<ScheduleEventId.EfCoreValueConverter>();
        configurationBuilder.Properties<ScheduleId>().HaveConversion<ScheduleId.EfCoreValueConverter>();
    }
    
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        var rows =  await SaveChangesAsync(cancellationToken);
        return rows > 0;
    }
}