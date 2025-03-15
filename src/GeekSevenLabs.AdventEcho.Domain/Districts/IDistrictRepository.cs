using GeekSevenLabs.AdventEcho.Kernel.Data;

namespace GeekSevenLabs.AdventEcho.Domain.Districts;

public interface IDistrictRepository : IRepository<District>
{
    void Add(District district);
    Task<District?> GetAsync(DistrictId id);
}