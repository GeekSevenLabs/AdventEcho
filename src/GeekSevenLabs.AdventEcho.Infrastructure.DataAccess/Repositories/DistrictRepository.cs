using GeekSevenLabs.AdventEcho.Domain.Districts;
using GeekSevenLabs.AdventEcho.Domain.Shared;
using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Contexts;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Repositories;

internal class DistrictRepository(AdventEchoDbContext db) : BaseRepository<District, DistrictId>(db), IDistrictRepository;