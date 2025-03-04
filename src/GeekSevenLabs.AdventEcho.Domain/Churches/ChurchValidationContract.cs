namespace GeekSevenLabs.AdventEcho.Domain.Churches;

public class ChurchValidationContract : Contract<Church>
{
    public const string NameRequiredMessage = $"The '{nameof(Church.Name)}' field is required";
    public const string NameIsLassThanThreeCharactersMessage = $"The '{nameof(Church.Name)}' field must be at least 3 characters";
    public const string DistrictIdRequiredMessage = $"The '{nameof(Church.DistrictId)}' id field is required";
    
    public ChurchValidationContract(Church church)
    {
        Requires()
            
            .IsNotNullOrEmpty(church.Name, nameof(Church.Name), NameRequiredMessage)
            .IsGreaterOrEqualsThan(church.Name, 3, nameof(Church.Name), NameIsLassThanThreeCharactersMessage)
            
            .IsNotEmpty(church.DistrictId.Value, nameof(Church.DistrictId), DistrictIdRequiredMessage);
    }
}