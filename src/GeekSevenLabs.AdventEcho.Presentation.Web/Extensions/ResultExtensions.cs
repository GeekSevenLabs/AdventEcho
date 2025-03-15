using GeekSevenLabs.AdventEcho.Kernel;

namespace GeekSevenLabs.AdventEcho.Presentation.Web.Extensions;

public static class ResultExtensions
{
    public static IResult ProcessResult<T>(this Result<T> result)
    {
        return result.Type switch
        {
            ResultType.Fail => Results.BadRequest(result.Error),
            ResultType.Forbidden => Results.Forbid(),
            ResultType.Success => Results.Ok(result.Value),
            ResultType.NotFound => Results.NotFound(result.Error),
            _ => Results.BadRequest(result.Error),
        };
    }
    
    public static IResult ProcessResult(this Result result)
    {
        return result.Type switch
        {
            ResultType.Fail => Results.BadRequest(result.Error),
            ResultType.Forbidden => Results.Forbid(),
            ResultType.Success => Results.Ok(),
            ResultType.NotFound => Results.NotFound(result.Error),
            _ => Results.BadRequest(result.Error),
        };
    }
}