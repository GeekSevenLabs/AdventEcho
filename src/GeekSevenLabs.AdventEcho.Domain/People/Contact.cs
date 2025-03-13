namespace GeekSevenLabs.AdventEcho.Domain.People;

public record ContactVo : ValueObject
{
    public ContactVo(string email, string phone)
    {
        Email = email;
        Phone = phone;

        // .IsNotNullOrEmpty(contact.Email, nameof(contact.Email), EmailRequiredMessage)
        // .IsEmail(contact.Email, nameof(contact.Email), EmailInvalidMessage)
        // .IsNotNullOrEmpty(contact.Phone, nameof(contact.Phone), PhoneRequiredMessage)
        // .Matches(contact.Phone, "^[0-9]{11}$", nameof(contact.Phone), PhoneInvalidMessage);
    }

    public string Email { get; private set; }
    public string Phone { get; private set; }
}