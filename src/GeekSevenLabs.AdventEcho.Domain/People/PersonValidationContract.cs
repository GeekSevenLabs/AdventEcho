namespace GeekSevenLabs.AdventEcho.Domain.People;

public class PersonValidationContract : Contract<Person>
{
    public const string NameIsRequiredMessage = $"{nameof(Person.Name)} is required";
    public const string ContactIsRequiredMessage = $"{nameof(Person.Contact)} is required";
    public const string ChurchIdIsRequiredMessage = $"{nameof(Person.ChurchId)} is required";
    
    public PersonValidationContract(Person person)
    {
        Requires()
            .IsNotNull(person.Name, nameof(Person.Name), NameIsRequiredMessage)
            .IsNotNull(person.Contact, nameof(Person.Contact), ContactIsRequiredMessage)
            .IsNotEmpty(person.ChurchId, nameof(Person.ChurchId), ChurchIdIsRequiredMessage);
    }
}