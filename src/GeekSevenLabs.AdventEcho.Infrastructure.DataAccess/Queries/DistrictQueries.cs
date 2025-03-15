using GeekSevenLabs.AdventEcho.Application.Districts;
using GeekSevenLabs.AdventEcho.Application.Districts.List;
using GeekSevenLabs.AdventEcho.Application.Shared.Districts;
using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Contexts;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Queries;

internal class DistrictQueries(AdventEchoDbContext db) : IDistrictQueries
{
    public Task<DistrictDto[]> ListAll(ListDistrictsQuery query)
    {
        return db
            .Districts
            .Include(district => district.Pastor)
            .AsNoTracking()
            .Select(district => new DistrictDto
            {
                Id = district.Id,
                Name = district.Name,
                PastorId = district.PastorId,
                PastorName = district.Pastor.Name.FullName,
            })
            .ToArrayAsync();
    }
}