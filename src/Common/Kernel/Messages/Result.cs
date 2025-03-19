namespace AdventEcho.Kernel.Messages;

public class Result
{
    public required ResultType Type { get; init; }
    public Dictionary<string, string[]> Errors { get; init; } = [];


    public static Result Failure(string error)
    {
        var result = new Result { Type = ResultType.Failure };
        result.Errors.Add(nameof(ResultType.Failure), [error]);
        return result;
    }
    
    public static Result Failure(string[] error)
    {
        var result = new Result { Type = ResultType.Failure };
        result.Errors.Add(nameof(ResultType.Failure), error);
        return result;
    }
    
    public static Result Failure(Dictionary<string, string[]> error)
    {
        return new Result
        {
            Type = ResultType.Failure,
            Errors = error
        };
    }

    public static Result Success()
    {
        return new Result { Type = ResultType.Success };
    }
}

public enum ResultType
{
    Success = 1,
    Failure = 11
}