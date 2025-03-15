namespace GeekSevenLabs.AdventEcho.Application.Shared.Districts.Editor;

public class EditorDistrictRequest
{
    public DistrictId? Id { get; set; }
    public string? Name { get; set; }
    public PersonId? PastorId { get; set; }
}