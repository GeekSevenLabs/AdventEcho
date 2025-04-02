using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
using AdventEcho.Kernel.Exceptions;

namespace AdventEcho.Kernel.Application.Shared.Services.Rests;



public class EchoRest
{
    public static IEchoRestDefinition Use(IHttpClientFactory factory)
    {
        return new EchoRestService(factory);
    }
}

public interface IEchoRestDefinition
{
    IEchoRestBuilder ForClient(string name);
}

public interface IEchoRestBuilder
{
    Task<Result> PostAsync<TRequest>(
        [StringSyntax(StringSyntaxAttribute.Uri)] string path,
        TRequest request,
        CancellationToken cancellationToken = default) where TRequest : notnull;
}

file class EchoRestService(IHttpClientFactory factory) : IEchoRestDefinition, IEchoRestBuilder
{
    private HttpClient _client = null!;
    private const string ProblemDetailsContentType = "application/problem+json";

    public IEchoRestBuilder ForClient(string name)
    {
        _client = factory.CreateClient(name);
        return this;
    }

    public async Task<Result> PostAsync<TRequest>(string path, TRequest request, CancellationToken cancellationToken = default) where TRequest : notnull
    {
        var requestJson = JsonSerializer.Serialize(request);
        var content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json");
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = content
        };

        return (await SendAsync(httpRequest, cancellationToken)).ToResult();
    }

    private async Task<Result<HttpResponseMessage>> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return Result<HttpResponseMessage>.Success(response);
            }

            if (response.Content.Headers.ContentType?.MediaType == ProblemDetailsContentType)
            {
                return await HandleProblemDetailsResponseAsync(response, cancellationToken);
            }

            return await HandlerProblemResponseAsync(response, cancellationToken);
        }
        catch (Exception e)
        {
            return EchoResults<HttpResponseMessage>.Fail(e);
        }
    }

    private static async Task<Result<HttpResponseMessage>> HandleProblemDetailsResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        try
        {
            var problemDetailsException = await response
                .Content
                .ReadFromJsonAsync<ProblemDetailsException>(cancellationToken);
            
            if (problemDetailsException is not null) return EchoResults<HttpResponseMessage>.Fail(problemDetailsException);

            return await HandlerProblemResponseAsync(response, cancellationToken);
        }
        catch (Exception e)
        {
            return EchoResults<HttpResponseMessage>.Fail(e);
        }
    }
    
    private static async Task<Result<HttpResponseMessage>> HandlerProblemResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var title = response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => "É necessário autenticar para realizar esta operação",
            HttpStatusCode.BadRequest => "Operação inválida",
            HttpStatusCode.Forbidden => "Sem permissão para realizar esta operação",
            _ => "Ocorreu um erro inesperado"
        };
        
        var error = await response.Content.ReadAsStringAsync(cancellationToken);
        var problem = new ProblemDetailsException("Bx0", title, (int)response.StatusCode, error, null);
        return EchoResults<HttpResponseMessage>.Fail(problem);
    }

}