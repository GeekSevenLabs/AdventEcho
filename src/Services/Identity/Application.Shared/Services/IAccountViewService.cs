using System.Net.Http.Json;
using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Kernel.Extensions;
using AdventEcho.Kernel.Messages;

namespace AdventEcho.Identity.Application.Shared.Services;

public interface IAccountViewService
{
    Task<Result<RefreshLoginResponse>> RefreshLoginAsync(Guid? refreshId = null,
        CancellationToken cancellationToken = default);
}

internal sealed class AccountViewService(IHttpClientFactory factory) : IAccountViewService
{
    private readonly HttpClient _client = factory.CreateClient(ApplicationConstants.HttpClients.AdventEchoIdentity);
    private const string BasePath = "api/v1/account/";

    public async Task<Result<RefreshLoginResponse>> RefreshLoginAsync(Guid? refreshId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _client.PostAsync($"{BasePath}/refresh-login", new StringContent(""), cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<RefreshLoginResponse>(cancellationToken: cancellationToken);
            
            response.EnsureSuccessStatusCode();
            
            if (result is null)
            {
                return "An error occurred while deserializing the response.".ToInvalidOperationException();
            }

            return result;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}