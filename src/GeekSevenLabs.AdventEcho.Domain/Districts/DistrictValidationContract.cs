

namespace GeekSevenLabs.AdventEcho.Domain.Districts;

public class DistrictValidationContract : Contract<District>
{
    public const string NameRequiredMessage = $"The '{nameof(District.Name)}' field is required";
    public const string PastorIdRequiredMessage = $"The '{nameof(District.PastorId)}' id field is required";
    
    public DistrictValidationContract(District district)
    {
        Requires()
            .IsNotNullOrEmpty(district.Name, nameof(District.Name), NameRequiredMessage)
            .IsNotEmpty(district.PastorId, nameof(District.PastorId), PastorIdRequiredMessage);
    }
}