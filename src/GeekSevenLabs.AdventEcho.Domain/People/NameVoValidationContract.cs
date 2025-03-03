namespace GeekSevenLabs.AdventEcho.Domain.People;

public class NameVoValidationContract : Contract<NameVo>
{
    public const string FirstNameIsRequired = $"{nameof(NameVo)}.{nameof(NameVo.First)} name is required";
    public const string FirstNameMinLength = $"{nameof(NameVo)}.{nameof(NameVo.First)} name must be at least 3 characters";
    public const string FirstNameMaxLength = $"{nameof(NameVo)}.{nameof(NameVo.First)} name must be at most 50 characters";
    
    public const string LastNameIsRequired = $"{nameof(NameVo)}.{nameof(NameVo.Last)} name is required";
    public const string LastNameMinLength = $"{nameof(NameVo)}.{nameof(NameVo.Last)} name must be at least 3 characters";
    public const string LastNameMaxLength = $"{nameof(NameVo)}.{nameof(NameVo.Last)} name must be at most 150 characters";
    
    public NameVoValidationContract(NameVo name)
    {        
        Requires()
            .IsNotNullOrEmpty(name.First, nameof(NameVo.First), FirstNameIsRequired)
            .IsGreaterOrEqualsThan(name.First, 3, nameof(NameVo.First), FirstNameMinLength)
            .IsLowerOrEqualsThan(name.First, 50, nameof(NameVo.First), FirstNameMaxLength)

            .IsNotNullOrEmpty(name.Last, nameof(NameVo.Last), LastNameIsRequired)
            .IsGreaterOrEqualsThan(name.Last, 3, nameof(NameVo.Last), LastNameMinLength)
            .IsLowerOrEqualsThan(name.Last, 100, nameof(NameVo.Last), LastNameMaxLength);

    }
}