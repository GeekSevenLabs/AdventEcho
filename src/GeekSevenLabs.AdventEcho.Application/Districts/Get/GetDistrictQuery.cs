using GeekSevenLabs.AdventEcho.Application.Shared.Districts;

namespace GeekSevenLabs.AdventEcho.Application.Districts.Get;

public class GetDistrictQuery : IQuery<DistrictDto>
{
    public required DistrictId Id { get; init; }
}