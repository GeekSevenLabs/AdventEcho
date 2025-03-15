using GeekSevenLabs.AdventEcho.Application.Districts.List;
using GeekSevenLabs.AdventEcho.Application.Shared.Districts;

namespace GeekSevenLabs.AdventEcho.Application.Districts;

public interface IDistrictQueries
{
    Task<DistrictDto[]> ListAll(ListDistrictsQuery query);
}