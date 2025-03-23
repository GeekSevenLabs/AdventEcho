using AdventEcho.Kernel.Messages;
using Microsoft.AspNetCore.Http;

namespace AdventEcho.Kernel.Server.Extensions;

public static class ResultExtensions
{
    public static async Task<IResult> ProcessResult(this Task<Result> resultTask)
    {
        var result = await resultTask;
        
        return result.Type switch
        {
            ResultType.Ok => Results.Ok(),
            ResultType.Fail => Results.ValidationProblem(result.Errors),
            _ => Results.BadRequest()
        };
    }
}