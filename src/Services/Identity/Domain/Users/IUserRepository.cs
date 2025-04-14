namespace AdventEcho.Identity.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
}