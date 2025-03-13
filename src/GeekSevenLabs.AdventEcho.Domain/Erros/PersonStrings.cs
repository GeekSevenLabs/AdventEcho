using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Domain.Erros;

public class PersonStrings
{
    public const string NameIsRequiredMessage = $"{nameof(Person.Name)} is required";
    public const string ContactIsRequiredMessage = $"{nameof(Person.Contact)} is required";
    public const string ChurchIdIsRequiredMessage = $"{nameof(Person.ChurchId)} is required";
}