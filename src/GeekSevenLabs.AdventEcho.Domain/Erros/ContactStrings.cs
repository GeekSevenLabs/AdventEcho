using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Domain.Erros;

public class ContactStrings
{
    public const string EmailRequiredMessage = $"{nameof(ContactVo.Email)} is required";
    public const string EmailInvalidMessage = $"{nameof(ContactVo.Email)} is invalid";
    public const string PhoneRequiredMessage = $"{nameof(ContactVo.Phone)} is required";
    public const string PhoneInvalidMessage = $"{nameof(ContactVo.Phone)} is invalid";
}