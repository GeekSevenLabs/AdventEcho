namespace AdventEcho.Identity.Application;

public interface IUserManager
{
    Task<Result> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default);
}