namespace AdventEcho.Identity.Domain.Users;

[HasPrivateEmptyConstructor]
public partial class User : IAggregateRoot
{
    // THIS ENTITY IS MANAGED BY THE IDENTITY SERVICE IN THE INFRASTRUCTURE LAYER
    
    public Guid Id { get; private set; }
    
    public string UserName { get; private set; } = string.Empty;
    public string NormalizedUserName { get; private set; } = string.Empty;
    
    public string Email { get; private set; } = string.Empty;
    public string NormalizedEmail { get; private set; } = string.Empty;

    public NameVo Name { get; private set; } = NameVo.Empty;
    public string? ConcurrencyStamp { get; private set; }
}