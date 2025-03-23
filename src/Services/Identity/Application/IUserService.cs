namespace AdventEcho.Identity.Application;

public interface IUserService
{
    Task<IUser?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    
    Task<Result<IUser>> LoginAsync(IUser user, string password, CancellationToken cancellationToken = default);
    Task<Result> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default);
}