namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

[HasPrivateEmptyConstructor]
public sealed partial class ScheduledPerson : Entity<ScheduledPerson, ScheduledPersonId>
{
    public ScheduledPerson(ScheduleDayId scheduleDayId, PersonId assignedId, ScheduleDayAssignmentId assignmentId)
    {
        ScheduleDayId = scheduleDayId;
        AssignedId = assignedId;
        AssignmentId = assignmentId;
        
        AddNotificationsAndThrow(new ScheduledPersonValidationContract(this));
    }
    public ScheduleDayId ScheduleDayId { get; private set; }
    public PersonId AssignedId { get; private set; }
    
    public ScheduleDayAssignmentId AssignmentId { get; private set; }
}