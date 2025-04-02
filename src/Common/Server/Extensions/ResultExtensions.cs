using System.Net;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
using AdventEcho.Kernel.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AdventEcho.Kernel.Server.Extensions;

public static class ResultExtensions
{
    public static async Task<IResult> ProcessResult(this Task<Result> resultTask)
    {
        var result = await resultTask;
        return result.Match(Ok, ExceptionToResult);
    }

    public static IResult ProcessResult(this Result result) => result.Match(Ok, ExceptionToResult);

    public static async Task<IResult> ProcessResult<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;
        return result.Match(Ok, ExceptionToResult);
    }

    public static IResult ProcessResult<T>(this Result<T> result) => result.Match(Ok, ExceptionToResult);

    private static IResult Ok() => Results.Ok();
    private static Ok<TValue> Ok<TValue>(TValue value) => TypedResults.Ok(value);

    private static IResult ExceptionToResult(Exception exception)
    {
        return exception switch
        {
            InvalidOperationException ex => ex.ToProblemResult(),
            ArgumentNullException ex => ex.ToProblemResult(),
            ArgumentException ex => ex.ToProblemResult(),
            
            NotFoundException ex => ex.ToProblemResult(HttpStatusCode.NotFound),
            ForbiddenException ex => ex.ToProblemResult(HttpStatusCode.Forbidden),
            BadRequestException ex => ex.ToProblemResult(HttpStatusCode.BadRequest),
            UnauthorizedException ex => ex.ToProblemResult(HttpStatusCode.Unauthorized),
            ConflictException ex => ex.ToProblemResult(HttpStatusCode.Conflict),
            ValidationException ex => Results.ValidationProblem(ex.Problems, statusCode: (int)HttpStatusCode.BadRequest),
            ProblemDetailsException ex => Results.ValidationProblem(
                type: ex.Type,
                title: ex.Title,
                statusCode: ex.Status,
                detail: ex.Detail,
                errors: ex.Errors), 

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

    private static IResult ToProblemResult(this Exception exception, HttpStatusCode? statusCode = null)
    {
        var type = exception.GetType().Name;
        var detail = exception.Message;
        return Results.Problem(detail: detail, type: type, statusCode: (int?)statusCode);
    }

    private record InternalServerErrorMessage(string Message);
}