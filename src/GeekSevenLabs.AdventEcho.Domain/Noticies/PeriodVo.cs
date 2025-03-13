namespace GeekSevenLabs.AdventEcho.Domain.Noticies;

public record PeriodVo : ValueObject
{
    public PeriodVo(DateTimeOffset startIn, DateTimeOffset endIn)
    {
        StartIn = startIn;
        EndIn = endIn;
        
        // Validations here
    }

    public DateTimeOffset StartIn { get; private set; }
    public DateTimeOffset EndIn { get; private set; }
}