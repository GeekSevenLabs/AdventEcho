namespace GeekSevenLabs.AdventEcho.Domain.People;

public record ContactVo : ValueObject
{
    public ContactVo(string email, string phone)
    {
        Email = email;
        Phone = phone;
        
        AddNotifications(new ContactVoValidationContract(this));
    }

    public string Email { get; private set; }
    public string Phone { get; private set; }
}