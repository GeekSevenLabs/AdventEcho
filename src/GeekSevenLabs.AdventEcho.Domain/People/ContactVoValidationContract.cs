namespace GeekSevenLabs.AdventEcho.Domain.People;

public class ContactVoValidationContract : Contract<ContactVo>
{
    public const string EmailRequiredMessage = $"{nameof(ContactVo.Email)} is required";
    public const string EmailInvalidMessage = $"{nameof(ContactVo.Email)} is invalid";
    public const string PhoneRequiredMessage = $"{nameof(ContactVo.Phone)} is required";
    public const string PhoneInvalidMessage = $"{nameof(ContactVo.Phone)} is invalid";
    
    public ContactVoValidationContract(ContactVo contact)
    {
        Requires()
            .IsNotNullOrEmpty(contact.Email, nameof(contact.Email), EmailRequiredMessage)
            .IsEmail(contact.Email, nameof(contact.Email), EmailInvalidMessage)
            .IsNotNullOrEmpty(contact.Phone, nameof(contact.Phone), PhoneRequiredMessage)
            .Matches(contact.Phone, "^[0-9]{11}$", nameof(contact.Phone), PhoneInvalidMessage);
    }
}