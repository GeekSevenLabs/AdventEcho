namespace GeekSevenLabs.AdventEcho.Domain.Districts;

[HasPrivateEmptyConstructor]
public sealed partial class District : Entity<District, DistrictId>
{
    public District(string name, PersonId pastorId)
    {
        Name = name;
        PastorId = pastorId;
        
        AddNotificationsAndThrow(new DistrictValidationContract(this));
    }

    public string Name { get; private set; }
    public PersonId PastorId { get; private set; }

    public void Update(string name, PersonId pastorId)
    {
        Name = name;
        PastorId = pastorId;
        AddNotificationsAndThrow(new DistrictValidationContract(this));
    }
}