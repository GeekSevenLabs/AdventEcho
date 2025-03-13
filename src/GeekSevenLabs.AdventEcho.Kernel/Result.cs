namespace GeekSevenLabs.AdventEcho.Kernel;

public class Result
{
    public bool Success { get; set; }
    
    public bool Failure => !Success;
    
    public string? Error { get; }
    public IDictionary<string, string[]> Errors { get; }
    
    protected Result(bool success, string? error = null)
    {
        Success = success;
        Error = error;
        Errors = new Dictionary<string, string[]>();
    }
    
    protected Result(bool success, IDictionary<string, string[]> errors)
    {
        Success = success;
        Errors = errors;
    }
    
    public static Result Ok() => new(true);
    public static Result Fail(string error) => new(false, error);
    public static Result Fail(IDictionary<string, string[]> errors) => new(false, errors);
    
    public static Result<T> Ok<T>(T value) => new(value, true, string.Empty);
    public static Result<T> Fail<T>(string error) => new(default, false, error);
    public static Result<T> Fail<T>(IDictionary<string, string[]> errors) => new(default, false, errors);
    
    // Convert value to result
    public static Result<T> FromValue<T>(T? value) => value != null ? Ok(value) : Fail<T>("Provided value is null.");
}

public class Result<T> : Result
{
    public T? Value { get; }
    
    internal Result(T? value, bool ok, string error) : base(ok, error) => Value = value;

    internal Result(T? value, bool ok, IDictionary<string, string[]> errors) : base(ok, errors) => Value = value;

    public static implicit operator Result<T>(T value) => FromValue(value);
    public static implicit operator T?(Result<T> result) => result.Value;
}