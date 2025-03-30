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

    public bool IsFail([NotNullWhen(true)] out Exception? error)
    {
        error = _error;
        return !IsSuccess;
    }
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
    
    public void Switch(Action<T> onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess(_value);
            return;
        }

        onFailure(_error);
    }
    
    public async Task SwitchAsync(Func<T, Task> onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess)
        {
            await onSuccess(_value);
            return;
        }

        onFailure(_error);
    }
    
    public TReturn Match<TReturn>(Func<T, TReturn> onSuccess, Func<Exception, TReturn> onFailure)
    {
        return IsSuccess ? onSuccess(_value) : onFailure(_error);
    }
    public async Task<TReturn> MatchAsync<TReturn>(Func<T, Task<TReturn>> onSuccess, Func<Exception, Task<TReturn>> onFailure)
    {
        return IsSuccess ? await onSuccess(_value) : await onFailure(_error);
    }
    public async Task<TReturn> MatchAsync<TReturn>(Func<T, Task<TReturn>> onSuccess, Func<Exception, TReturn> onFailure)
    {
        return IsSuccess ? await onSuccess(_value) : onFailure(_error);
    }
    
    public bool IsFail([NotNullWhen(false)] out T? value, [NotNullWhen(true)] out Exception? error)
    {
        value = _value;
        error = _error;
        return !IsSuccess;
    }

    // Helper methods for constructing the `Result<T>`
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Fail(Exception error) => new(error);
    
    
    // Allow converting a T directly into Result<T>
    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Exception error) => Fail(error);
}