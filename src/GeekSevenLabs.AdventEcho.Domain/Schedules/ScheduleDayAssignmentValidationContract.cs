namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

public class ScheduleDayAssignmentValidationContract : Contract<ScheduleDayAssignment>
{
    public const string NameRequiredMessage = 
        $"The '{nameof(ScheduleDayAssignment.Name)}' field is required";

    public const string ForScheduleTypeRequiredMenssage =
        $"The '{nameof(ScheduleDayAssignment.ForScheduleType)} field is required'";
    
    public ScheduleDayAssignmentValidationContract(ScheduleDayAssignment scheduleDayAssignment)
    {
        Requires()
            .IsNotNullOrEmpty(scheduleDayAssignment.Name, nameof(ScheduleDayAssignment.Name), NameRequiredMessage)
            .IsNotNull(scheduleDayAssignment.ForScheduleType , nameof(ScheduleDayAssignment.ForScheduleType),ForScheduleTypeRequiredMenssage);
    }
}