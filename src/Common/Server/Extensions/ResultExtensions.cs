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
            ResultType.Success => Results.Ok(),
            ResultType.Failure => Results.ValidationProblem(result.Errors),
            _ => Results.BadRequest()
        };
    }
}