using AdventEcho.Kernel.Messages;

namespace AdventEcho.Identity.Domain.Users.Services;

public interface IUserService
{
    Task<Result> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default);
}