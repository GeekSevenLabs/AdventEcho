using AdventEcho.Identity.Domain.Users;
using AdventEcho.Kernel.Application.Shared.Messages.Results;

namespace AdventEcho.Identity.Application.Services.Users;

public interface IUserService
{
    Task<Result> RegisterAsync(NameVo name, string email, string password, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmailAsync(Guid userId, string token, CancellationToken cancellationToken = default);
    Task<Result<User>> CheckPasswordSignInAsync(string email, string password, CancellationToken cancellationToken = default);
}