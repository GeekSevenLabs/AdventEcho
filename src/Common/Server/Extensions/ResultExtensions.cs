using System.Net;
using AdventEcho.Kernel.Exceptions;
using AdventEcho.Kernel.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AdventEcho.Kernel.Server.Extensions;

public static class ResultExtensions
{
    public static async Task<IResult> ProcessResult(this Task<Result> resultTask)
    {
        var result = await resultTask;
        return result.Match(Ok, ExceptionToResult);
    }
    
    private static IResult Ok() => Results.Ok();
    
    private static IResult ExceptionToResult(Exception exception)
    {
        return exception switch
        {
            InvalidOperationException ex => TypedResults.ValidationProblem(ex.ToUniqueProblem(), type: nameof(InvalidOperationException)),
            ArgumentNullException ex => TypedResults.ValidationProblem(ex.ToUniqueProblem(), type: nameof(ArgumentNullException)),
            AdventEchoValidationException ex => TypedResults.ValidationProblem(ex.Problems, type: nameof(AdventEchoValidationException)),
            
            _ => TypedResults.InternalServerError(new InternalServerErrorMessage(exception.Message))
        };
    }

    public static RouteHandlerBuilder AddCommonProduces(this RouteHandlerBuilder builder)
    {
        return builder
            .AddProduces<InternalServerErrorMessage>(HttpStatusCode.InternalServerError)
            .ProducesValidationProblem();
    }
    
    private static RouteHandlerBuilder AddProduces<TResult>(this RouteHandlerBuilder builder, HttpStatusCode statusCode)
    {
        return builder.Produces<TResult>((int)HttpStatusCode.BadRequest);
    }

    private static Dictionary<string, string[]> ToUniqueProblem(this Exception exception)
    {
        return new Dictionary<string, string[]> { { exception.GetType().Name, [ exception.Message ] } };
    }
    
    private record InternalServerErrorMessage(string Message);
}