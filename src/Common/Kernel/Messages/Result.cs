namespace AdventEcho.Kernel.Messages;

public class Result
{
    public required ResultType Type { get; init; }
    public Dictionary<string, string[]> Errors { get; init; } = [];


    public static Result Fail(string error)
    {
        var result = new Result { Type = ResultType.Fail };
        result.Errors.Add(nameof(ResultType.Fail), [error]);
        return result;
    }
    
    public static Result<TValue> Fail<TValue>(string error)
    {
        var result = new Result<TValue> { Type = ResultType.Fail };
        result.Errors.Add(nameof(ResultType.Fail), [error]);
        return result;
    }
    
    public static Result Fail(string[] error)
    {
        var result = new Result { Type = ResultType.Fail };
        result.Errors.Add(nameof(ResultType.Fail), error);
        return result;
    }
    
    public static Result<TValue> Fail<TValue>(string[] error)
    {
        var result = new Result<TValue> { Type = ResultType.Fail };
        result.Errors.Add(nameof(ResultType.Fail), error);
        return result;
    }
    
    public static Result Fail(Dictionary<string, string[]> error)
    {
        return new Result
        {
            Type = ResultType.Fail,
            Errors = error
        };
    }
    
    public static Result<TValue> Fail<TValue>(Dictionary<string, string[]> error)
    {
        return new Result<TValue>
        {
            Type = ResultType.Fail,
            Errors = error
        };
    }

    public static Result Ok()
    {
        return new Result { Type = ResultType.Ok };
    }
    
    public static Result<TValue> Ok<TValue>(TValue value)
    {
        return new Result<TValue> { Type = ResultType.Ok, Value = value};
    }
    
    // Convert value to result
    public static Result<T> FromValue<T>(T? value) => value != null ? Ok(value) : Fail<T>("Provided value is null.");
}

public class Result<T> : Result
{
    public  T? Value { get; init; }

    public static implicit operator Result<T>(T value) => FromValue(value);
    public static implicit operator T?(Result<T> result) => result.Value;
}

public enum ResultType
{
    Ok = 1,
    Fail = 11
}