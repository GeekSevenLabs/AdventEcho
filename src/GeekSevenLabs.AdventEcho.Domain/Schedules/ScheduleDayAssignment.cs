using GeekSevenLabs.AdventEcho.Domain.Shared.Enums;

namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

[HasPrivateEmptyConstructor]
public sealed partial class ScheduleDayAssignment : Entity<ScheduleDayAssignment, ScheduleDayAssignmentId>
{
    public ScheduleDayAssignment(string name, ScheduleType forScheduleType)
    {
        Name = name;
        ForScheduleType = forScheduleType;
        AddNotificationsAndThrow(new ScheduleDayAssignmentValidationContract(this));
    }

    public string Name { get; private set; }
    public ScheduleType ForScheduleType { get; set; }
}