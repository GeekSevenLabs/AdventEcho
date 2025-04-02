using AdventEcho.Identity.Application.Shared.Accounts.Register;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
using AdventEcho.Kernel.Application.Shared.Services.Rests;

namespace AdventEcho.Identity.Application.Shared.Services;

internal class AccountViewService(IHttpClientFactory factory) : IAccountViewService
{
    private readonly IEchoRestBuilder _rest = EchoRest.Use(factory).ForClient(ApplicationSharedConstants.HttpClients.AdventEchoIdentityName);
    
    public async Task<Result> RegisterAsync(RegisterAccountRequest request, CancellationToken cancellationToken)
    {
        return await _rest.PostAsync("api/v1/accounts/register", request, cancellationToken);
    }
}