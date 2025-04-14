namespace AdventEcho.Registration.Domain.People;

[HasPrivateEmptyConstructor]
public partial class Person : Entity, IAggregateRoot
{
    public NameVo Name { get; private set; }
    public string Email { get; private set; }
}