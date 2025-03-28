using System.Diagnostics.CodeAnalysis;

namespace AdventEcho.Kernel.Messages;

/*
 * Caution: This is a simple implementation of the Result pattern.
 * For more advanced scenarios, consider using a library.
 * This implementation was created just to learn the pattern.
 */


public class Result
{
    private readonly Exception? _error;
    
    private Result(Exception error)
    {
        IsSuccess = false;
        _error = error;
    }   
    
    private Result()
    {
        IsSuccess = true;
        _error = null;
    }
    
    [MemberNotNullWhen(false, nameof(_error))]
    private bool IsSuccess { get; }
    
    public Result Switch(Action onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess();
            return this;
        }

        onFailure(_error);
        return this;
    }
    
    public void Match(Action onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess();
        }
        else
        {
            onFailure(_error);
        }
    }
    
    public TReturn Match<TReturn>(Func<TReturn> onSuccess, Func<Exception, TReturn> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(_error);
    }
    
    public static Result Success() => new();
    public static Result Fail(Exception error) => new(error);
    
    public static implicit operator Result(Exception error) => Fail(error);
}

public class Result<T>
{
    // We don't expose these publicly
    private readonly T? _value;
    private readonly Exception? _error;
    
    // Success constructor
    private Result(T value)
    {
        IsSuccess = true;
        _value = value;
        _error = null;
    }

    // Failure constructor
    private Result(Exception error)
    {
        IsSuccess = false;
        _value = default;
        _error = error;
    }

    [MemberNotNullWhen(true, nameof(_value))]
    [MemberNotNullWhen(false, nameof(_error))]
    private bool IsSuccess { get; }
    
    public Result<TReturn> Switch<TReturn>(Func<T, TReturn> onSuccess, Func<Exception, Exception> onFailure)
    {
        if (IsSuccess)
        {
            return onSuccess(_value);
        }

        var err = onFailure(_error);
        return Result<TReturn>.Fail(err);
    }
    
    public TReturn Match<TReturn>(Func<T, TReturn> onSuccess, Func<Exception, TReturn> onFailure)
    {
        return IsSuccess ? onSuccess(_value) : onFailure(_error);
    }
    

    // Helper methods for constructing the `Result<T>`
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Fail(Exception error) => new(error);
    
    
    // Allow converting a T directly into Result<T>
    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Exception error) => Fail(error);
}