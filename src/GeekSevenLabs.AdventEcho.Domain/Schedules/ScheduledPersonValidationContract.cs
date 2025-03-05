namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

public class ScheduledPersonValidationContract : Contract<ScheduledPerson>
{
    public const string ScheduleDayIdRequiredMessage = $"The '{nameof(ScheduledPerson.ScheduleDayId)}' id field is required";
    public const string AssignedIdRequiredMessage = $"The '{nameof(ScheduledPerson.AssignedId)} id field is required'";
    public const string AssignmentIdRequiredMessage = $"The '{nameof(ScheduledPerson.AssignedId)} id field is required'";
    
    public ScheduledPersonValidationContract(ScheduledPerson scheduledPerson)
    {
        Requires()
            .IsNotEmpty(scheduledPerson.ScheduleDayId.Value, nameof(scheduledPerson.ScheduleDayId), ScheduleDayIdRequiredMessage)
            .IsNotEmpty(scheduledPerson.AssignedId.Value, nameof(scheduledPerson.AssignedId), AssignedIdRequiredMessage)
            .IsNotEmpty(scheduledPerson.AssignmentId.Value, nameof(scheduledPerson.AssignmentId), AssignmentIdRequiredMessage);
    }
}