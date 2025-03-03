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

    public void Update(string name, DistrictId districtId)
    {
        Name = name;
        DistrictId = districtId;
        
        AddNotificationsAndThrow(new ChurchValidationContract(this));
    }
}