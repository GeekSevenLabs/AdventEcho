namespace GeekSevenLabs.AdventEcho.Kernel;

public class Result
{
    public bool Success => Type == ResultType.Success;
    
    public ResultType Type { get; private init; }
    public string Error { get; private init; } = string.Empty;
    
    
    public static Result Ok() => new() { Type = ResultType.Success };
    public static Result NotFound(string error = "Not found") => new() { Type = ResultType.NotFound, Error = error };
    public static Result Forbidden() => new() { Type = ResultType.Forbidden };
    public static Result Fail(string error) => new() { Type = ResultType.Fail, Error = error };
    
    public static Result<T> Ok<T>(T value) => new() { Type = ResultType.Success, Value = value };
    public static Result<T> NotFound<T>(string error = "Not found") => new() { Type = ResultType.NotFound, Error = error };
    public static Result<T> Forbidden<T>() => new() { Type = ResultType.Forbidden };
    public static Result<T> Fail<T>(string error) => new() { Type = ResultType.Fail, Error = error };
    
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
    Success,
    NotFound,
    Forbidden,
    Fail
}