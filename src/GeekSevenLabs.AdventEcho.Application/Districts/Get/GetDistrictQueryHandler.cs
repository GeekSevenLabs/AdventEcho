using GeekSevenLabs.AdventEcho.Application.Shared.Districts;
using GeekSevenLabs.AdventEcho.Domain.Districts;

namespace GeekSevenLabs.AdventEcho.Application.Districts.Get;

public class GetDistrictQueryHandler(IDistrictRepository districtRepository) : IQueryHandler<GetDistrictQuery, DistrictDto>
{
    public async Task<Result<DistrictDto>> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
    {
        var district = await districtRepository.GetAsync(request.Id);
        if(district is null) return Result.NotFound<DistrictDto>("District not found.");
        
        return Result.Ok(new DistrictDto
        {
            Id = district.Id,
            Name = district.Name,
            PastorId = district.PastorId
        });
    }
}