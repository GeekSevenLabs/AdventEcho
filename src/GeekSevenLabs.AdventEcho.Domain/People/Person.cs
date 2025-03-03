namespace GeekSevenLabs.AdventEcho.Domain.People;

[HasPrivateEmptyConstructor]
public sealed partial class Person : Entity<Person, PersonId>
{
    public Person(NameVo name, ContactVo contact, ChurchId churchId)
    {
        Name = name;
        Contact = contact;
        ChurchId = churchId;

        AddNotifications(name);
        AddNotifications(contact);
        AddNotificationsAndThrow(new PersonValidationContract(this));
    }

    public NameVo Name { get; private set; }
    public ContactVo Contact { get; private set; }
    public ChurchId ChurchId { get; private set; }
}