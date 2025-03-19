using AdventEcho.Identity.Application.Shared;
using AdventEcho.Identity.Application.Shared.Register;
using AdventEcho.Kernel.Extensions;

namespace AdventEcho.Identity.Application;

public class AccountService(IUserManager userManager) : IAccountService
{
    public async Task<Result> RegisterAsync(RegisterUserRequest request)
    {
        return await userManager.RegisterAsync(request.Name.Required(), request.Email.Required(), request.Password.Required());
    }
}