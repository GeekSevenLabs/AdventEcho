namespace GeekSevenLabs.AdventEcho.Application.Shared.Districts;

public class DistrictDto
{
    public required DistrictId Id { get; init; }
    public required string Name { get; init; }
    public required PersonId PastorId { get; init; }


    // For Frontend Showing
    public string PastorName { get; set; } = string.Empty;
}