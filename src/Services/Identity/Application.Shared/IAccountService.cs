using AdventEcho.Identity.Application.Shared.Register;
using AdventEcho.Kernel.Messages;

namespace AdventEcho.Identity.Application.Shared;

public interface IAccountService
{
    Task<Result> RegisterAsync(RegisterUserRequest request);
    
}