namespace GeekSevenLabs.AdventEcho.Domain.Districts;

[HasPrivateEmptyConstructor]
public sealed partial class District : Entity<District>
{
    public District(string name, Guid pastorId)
    {
        Name = name;
        PastorId = pastorId;
        
        AddNotificationsAndThrow(new DistrictValidationContract(this));
    }

    public string Name { get; private set; }
    public Guid PastorId { get; private set; }

    public void ChangePastor(Guid pastorId) => PastorId = pastorId;

    public void Update(string name, Guid pastorId)
    {
        Name = name;
        PastorId = pastorId;
        AddNotificationsAndThrow(new DistrictValidationContract(this));
    }
}