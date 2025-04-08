using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Identity.Application.Shared.Accounts.Register;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Kernel.Application.Shared.Services.Rests;

namespace AdventEcho.Identity.Application.Shared.Services;

using static Constants.Routes.Account;

internal class AccountViewService(IHttpClientFactory factory) : IAccountViewService
{
    private readonly IEchoRestBuilder _rest = EchoRest.Use(factory).ForClient(ApplicationSharedConstants.HttpClients.AdventEchoIdentityName);
    
    public async Task<Result> RegisterAsync(RegisterAccountRequest request, CancellationToken cancellationToken)
    {
        return await _rest.PostAsync(Register.Endpoint, request, cancellationToken);
    }

    public async Task<Result<RefreshLoginAccountResponse>> RefreshLoginAsync(RefreshLoginAccountRequest request, CancellationToken cancellationToken)
    {
        return await _rest.PostAsync<RefreshLoginAccountRequest, RefreshLoginAccountResponse>(Refresh.Endpoint, request, cancellationToken);
    }

    public async Task<Result<LoginAccountResponse>> LoginAsync(LoginAccountRequest request, CancellationToken cancellationToken)
    {
        return await _rest.PostAsync<LoginAccountRequest, LoginAccountResponse>(Login.Endpoint, request, cancellationToken);
    }
}