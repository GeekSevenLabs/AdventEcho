using GeekSevenLabs.AdventEcho.Domain.Churches;

namespace GeekSevenLabs.AdventEcho.Domain.Erros;

public static class ChurchStrings
{
    public const string NameRequiredMessage = $"The '{nameof(Church.Name)}' field is required";
    public const string NameIsLassThanThreeCharactersMessage = $"The '{nameof(Church.Name)}' field must be at least 3 characters";
    public const string DistrictIdRequiredMessage = $"The '{nameof(Church.DistrictId)}' id field is required";
    
}