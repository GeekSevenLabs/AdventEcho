namespace GeekSevenLabs.AdventEcho.Application.Shared.Districts;

public class CreateDistrictRequest
{
    public string? Name { get; set; }
    public PersonId? PastorId { get; set; }
}