using GeekSevenLabs.AdventEcho.Domain.Shared.Enums;

namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

[HasPrivateEmptyConstructor]
public sealed partial class Schedule : Entity<Schedule, ScheduleId>
{
    public readonly List<ScheduleDay> _days = []; 
    
    public ScheduleType Type { get; private set; }
    public ScheduleStatus Status { get; private set; }
    
    public DistrictId? DistrictId { get; private set; }
    public ChurchId? ChurchId { get; private set; }

    public IReadOnlyList<ScheduleDay> Days => _days.AsReadOnly();
}