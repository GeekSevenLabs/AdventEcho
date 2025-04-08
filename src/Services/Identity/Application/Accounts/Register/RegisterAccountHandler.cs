using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Application.Shared.Accounts.Register;
using AdventEcho.Identity.Domain.Users;
using AdventEcho.Kernel.Application.Handlers;

namespace AdventEcho.Identity.Application.Accounts.Register;

internal class RegisterAccountHandler(IUserService service) : ICommandHandler<RegisterAccountRequest>
{
    public async Task<Result> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
    {
        var name = new NameVo(request.FirstName!, request.LastName!);
        return await service.RegisterAsync(name, request.Email!, request.Password!, cancellationToken);
    }
}