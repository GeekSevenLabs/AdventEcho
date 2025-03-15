using GeekSevenLabs.AdventEcho.Domain.Churches;
using GeekSevenLabs.AdventEcho.Domain.Erros;
using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Domain.Districts;

[HasPrivateEmptyConstructor]
public sealed partial class District : Entity<DistrictId>, IAggregateRoot
{
    private readonly List<Church> _churches = [];
    
    public District(string name, PersonId pastorId)
    {
        Name = name;
        PastorId = pastorId;

        Throw.When.NullOrEmpty(Name, DistrictString.NameRequiredMessage);
        Throw.When.Empty(PastorId, DistrictString.PastorIdRequiredMessage);
    }

    public string Name { get; private set; }
    public PersonId PastorId { get; private set; }

    public IReadOnlyList<Church> Churches => _churches.AsReadOnly();

    #region Navigations
    // (Exclusive for queries, dont use in domain logic)
    // TODO :: After create a ReadOnly for queries, remove this property

    public Person Pastor { get; set; } = null!; // EF Core Navigation Property

    #endregion
    
    public void Update(string name, PersonId pastorId)
    {
        Throw.When.NullOrEmpty(name, DistrictString.NameRequiredMessage);
        Throw.When.Empty(pastorId, DistrictString.PastorIdRequiredMessage);
        
        Name = name;
        PastorId = pastorId;
    }
}