using GeekSevenLabs.AdventEcho.Domain.Districts;

namespace GeekSevenLabs.AdventEcho.Domain.Churches;

[HasPrivateEmptyConstructor]
public sealed partial class Church : Entity<Church, ChurchId>
{
    public Church(string name, DistrictId districtId)
    {
        Name = name;
        DistrictId = districtId;
        
        AddNotificationsAndThrow(new ChurchValidationContract(this));
    }

    public string Name { get; private set; }
    public DistrictId DistrictId { get; private set; }

    public District District { get; private set; } = null!;

    public void Update(string name, DistrictId districtId)
    {
        Name = name;
        DistrictId = districtId;
        
        AddNotificationsAndThrow(new ChurchValidationContract(this));
    }
}