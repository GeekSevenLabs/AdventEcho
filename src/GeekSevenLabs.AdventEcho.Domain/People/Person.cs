using GeekSevenLabs.AdventEcho.Domain.Schedules;

namespace GeekSevenLabs.AdventEcho.Domain.People;

[HasPrivateEmptyConstructor]
public sealed partial class Person : Entity<PersonId>
{
    private readonly List<ScheduledPerson> _scheduledPeople = [];
    private readonly List<ScheduleDay> _scheduleDays = [];
    
    public Person(NameVo name, DocumentVo document, ContactVo contact, ChurchId churchId)
    {
        Name = name;
        Document = document;
        Contact = contact;
        ChurchId = churchId;

        // .IsNotNull(person.Name, nameof(Person.Name), NameIsRequiredMessage)
        // .IsNotNull(person.Contact, nameof(Person.Contact), ContactIsRequiredMessage)
        // .IsNotEmpty(person.ChurchId.Value, nameof(Person.ChurchId), ChurchIdIsRequiredMessage);
    }

    public NameVo Name { get; private set; }
    public ContactVo Contact { get; private set; }
    public DocumentVo Document { get; private set; }
    
    public ChurchId? ChurchId { get; private set; }
    
    public IReadOnlyList<ScheduledPerson> ScheduledPeople => _scheduledPeople.AsReadOnly();
    public IReadOnlyList<ScheduleDay> ScheduleDays => _scheduleDays.AsReadOnly();
    
}