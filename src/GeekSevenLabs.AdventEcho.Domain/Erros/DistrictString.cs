using GeekSevenLabs.AdventEcho.Domain.Districts;

namespace GeekSevenLabs.AdventEcho.Domain.Erros;

public static class DistrictString
{
    public const string NameRequiredMessage = $"The '{nameof(District.Name)}' field is required";
    public const string PastorIdRequiredMessage = $"The '{nameof(District.PastorId)}' id field is required";
}