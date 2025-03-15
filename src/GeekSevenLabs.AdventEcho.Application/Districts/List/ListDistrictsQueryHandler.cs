using GeekSevenLabs.AdventEcho.Application.Shared.Districts;

namespace GeekSevenLabs.AdventEcho.Application.Districts.List;

public class ListDistrictsQueryHandler(
    IDistrictQueries districtQueries)
    : IQueryHandler<ListDistrictsQuery, DistrictDto[]>
{
    public async Task<Result<DistrictDto[]>> Handle(ListDistrictsQuery request, CancellationToken cancellationToken)
    {
        return Result.Ok(await districtQueries.ListAll(request));
    }
}