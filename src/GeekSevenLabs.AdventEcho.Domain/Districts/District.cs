using GeekSevenLabs.AdventEcho.Domain.Churches;

namespace GeekSevenLabs.AdventEcho.Domain.Districts;

[HasPrivateEmptyConstructor]
public sealed partial class District : Entity<DistrictId>, IAggregateRoot
{
    private readonly List<Church> _churches = [];
    
    public District(string name, PersonId pastorId)
    {
        Name = name;
        PastorId = pastorId;

        // .IsNotNullOrEmpty(district.Name, nameof(District.Name), NameRequiredMessage)
        // .IsNotEmpty(district.PastorId.Value, nameof(District.PastorId), PastorIdRequiredMessage);
    }

    public string Name { get; private set; }
    public PersonId PastorId { get; private set; }

    public IReadOnlyList<Church> Churches => _churches.AsReadOnly();
    
    public void Update(string name, PersonId pastorId)
    {
        Name = name;
        PastorId = pastorId;
        
        // .IsNotNullOrEmpty(district.Name, nameof(District.Name), NameRequiredMessage)
        // .IsNotEmpty(district.PastorId.Value, nameof(District.PastorId), PastorIdRequiredMessage);
    }
}