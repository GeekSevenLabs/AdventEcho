namespace GeekSevenLabs.AdventEcho.Domain.Churches;

[HasPrivateEmptyConstructor]
public sealed partial class Church : Entity<Church>
{
    public Church(string name, Guid districtId)
    {
        Name = name;
        DistrictId = districtId;
        
        AddNotificationsAndThrow(new ChurchValidationContract(this));
    }

    public string Name { get; private set; }
    public Guid DistrictId { get; private set; }

    public void Update(string name, Guid districtId)
    {
        Name = name;
        DistrictId = districtId;
        
        AddNotificationsAndThrow(new ChurchValidationContract(this));
    }
}