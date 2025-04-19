namespace AdventEcho.Registration.Domain.People;

[HasPrivateEmptyConstructor]
public partial class Person : Entity, IAggregateRoot
{
    public Person(NameVo name, string email, string phone, Guid userId)
    {
        Name = name;
        Email = email;
        Phone = phone;
        UserId = userId;
    }
    
    public Person(NameVo name, DateOnly birthDate, string email, string phone, Guid userId)
    {
        Name = name;
        BirthDate = birthDate;
        Email = email;
        Phone = phone;
        UserId = userId;
    }
    
    public NameVo Name { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    
    public string Email { get; private set; }
    public string Phone { get; private set; }
    
    public Guid UserId { get; private set; }
}