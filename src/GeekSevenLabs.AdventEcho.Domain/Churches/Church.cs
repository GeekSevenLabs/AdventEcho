using GeekSevenLabs.AdventEcho.Domain.Districts;
using GeekSevenLabs.AdventEcho.Domain.Erros;

namespace GeekSevenLabs.AdventEcho.Domain.Churches;

[HasPrivateEmptyConstructor]
public sealed partial class Church : Entity<ChurchId>
{
    public Church(string name, DistrictId districtId)
    {
        Name = name;
        DistrictId = districtId;

        Throw.When.NullOrEmpty(Name, ChurchStrings.NameRequiredMessage);
        // TODO: New feature for Menso.Tools.Exceptions
        // Throw.When.LessThan(Name.Length, 3, ChurchStrings.NameIsLassThanThreeCharactersMessage);
        Throw.When.NullOrEmpty(DistrictId.Value, ChurchStrings.DistrictIdRequiredMessage);
    }

    public string Name { get; private set; }
    public DistrictId DistrictId { get; private set; }

    #region Navigations

    public District District { get; private set; } = null!;

    #endregion
}