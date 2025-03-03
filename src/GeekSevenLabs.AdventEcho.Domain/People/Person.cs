namespace GeekSevenLabs.AdventEcho.Domain.People;

[HasPrivateEmptyConstructor]
public sealed partial class Person : Entity<Person>
{
    public Person(NameVo name, ContactVo contact, Guid churchId)
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
    public Guid ChurchId { get; private set; }
}