using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

// ReSharper disable once CheckNamespace
namespace AdventEcho;

public static class ResultExtensions
{
    private const string ErrorCodeDefault = "UnknownError";
    private const string Type = "EchoResult";
    
    public static async Task<IResult> ProcessResult(this Task<Result> resultTask) => (await resultTask).Match(Ok, ErrorsToProblemDetails);
    public static IResult ProcessResult(this Result result) => result.Match(Ok, ErrorsToProblemDetails);

    public static async Task<IResult> ProcessResult<T>(this Task<Result<T>> resultTask) => (await resultTask).Match(Ok, ErrorsToProblemDetails);
    public static IResult ProcessResult<T>(this Result<T> result) => result.Match(Ok, ErrorsToProblemDetails);

    private static IResult Ok(IEnumerable<IEchoSuccess> successes) => Results.Ok(); // ? Fazer algo com os successes
    private static Ok<TValue> Ok<TValue>(TValue value) => TypedResults.Ok(value);


    private static IResult ErrorsToProblemDetails(IEnumerable<IEchoError> errors)
    {
        var errorsDic = errors
            .Select(ToErrorItem)
            .GroupBy(item => item.Code)
            .ToDictionary(grouping => grouping.Key, grouping => grouping.SelectMany(e => e.Messages).ToArray());
        
        return Results.ValidationProblem(errorsDic, type: Type);
    }

    private static ErrorItem ToErrorItem(IEchoError error)
    {
        return error switch
        {
            IExceptionalError exceptionalError => ExceptionalError(exceptionalError),
            _ => Default(error)
        };
    }

    private static ErrorItem Default(IEchoError error)
    {
        return new ErrorItem(GetErrorCodeOrDefault(error), [error.Message]);
    }

    private static ErrorItem ExceptionalError(IExceptionalError error)
    {
        return new ErrorItem(GetErrorCodeOrDefault(error), ExtractErrorMessageFromException(error.Exception));
    }

    private static string[] ExtractErrorMessageFromException(Exception exception)
    {
        // TODO: Add more specific exception handling
        return [exception.Message];
    }

    private static string GetErrorCodeOrDefault(IEchoError error)
    {
        return error.Metadata.GetValueOrDefault(ResultConstants.Metadata.ErrorCode)?.ToString() ?? ErrorCodeDefault;
    }

    private record ErrorItem(string Code, string[] Messages);

}