using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Domain.Erros;

public class NameStrings
{
    public const string FirstNameIsRequired = $"{nameof(NameVo)}.{nameof(NameVo.First)} name is required";
    public const string FirstNameMinLength = $"{nameof(NameVo)}.{nameof(NameVo.First)} name must be at least 3 characters";
    public const string FirstNameMaxLength = $"{nameof(NameVo)}.{nameof(NameVo.First)} name must be at most 50 characters";
    public const string LastNameIsRequired = $"{nameof(NameVo)}.{nameof(NameVo.Last)} name is required";
    public const string LastNameMinLength = $"{nameof(NameVo)}.{nameof(NameVo.Last)} name must be at least 3 characters";
    public const string LastNameMaxLength = $"{nameof(NameVo)}.{nameof(NameVo.Last)} name must be at most 150 characters";
}