using System.Diagnostics.CodeAnalysis;

namespace AdventEcho;

public abstract class EchoResultBase<TResult> where TResult : EchoResultBase<TResult>
{
    protected readonly List<IEchoReason> Reasons = [];

    #region Properties

    public IReadOnlyCollection<IEchoError> Errors => Reasons.OfType<IEchoError>().ToList();
    public IReadOnlyCollection<IEchoSuccess> Successes => Reasons.OfType<IEchoSuccess>().ToList();
    
    public virtual bool IsFailure => Errors.Count > 0;
    public virtual bool IsSuccess => Errors.Count == 0 && Successes.Count > 0;
    
    #endregion
    
    #region Behaviors

    public TReturn Match<TReturn>(Func<IEnumerable<IEchoSuccess>, TReturn> onSuccess, Func<IEnumerable<IEchoError>, TReturn> onFailure)
    {
        return IsSuccess ? onSuccess(Successes) : onFailure(Errors);
    }
    
    public bool IsFail(out List<IEchoError> errors)
    {
        if (IsFailure)
        {
            errors = Errors.ToList();
            return true;
        }
        
        errors = [];
        return false;
    }
    
    public async Task WhenSuccessAsync(Func<Task> onSuccess)
    {
        if (IsSuccess) await onSuccess();
    }
    
    public async Task WhenFailureAsync(Func<IEchoError[], Task> onFailure)
    {
        if (IsFailure) await onFailure(Errors.ToArray());
    }
    
    internal virtual TResult AddReason(IEchoReason reason)
    {
        Reasons.Add(reason);
        return (TResult)this;
    }

    internal virtual TResult AddReasons(IEnumerable<IEchoReason> reasons)
    {
        Reasons.AddRange(reasons);
        return (TResult)this;
    }
    
    #endregion
    
}


public sealed class Result : EchoResultBase<Result>
{
    
    #region Behaviors

    public static Result Ok() => new Result().AddReason(EchoSuccess.Ok());
    
    public static Result Fail(IEnumerable<IEchoError> errors) => new Result().AddReasons(errors);
    public static Result Fail(Exception exception) => new Result().AddReason(new EchoExceptionalError(exception));

    #endregion

    #region Implicit Operators
    
    // Cast Error to Result<T> 
    public static implicit operator Result(Exception exception) => Fail(exception);
    public static implicit operator Result(List<IEchoError> errors) => Fail(errors);
    public static implicit operator Result(IEchoError[] errors) => Fail(errors);

    #endregion
    
    
}

public sealed class Result<TValue> : EchoResultBase<Result<TValue>>
{
    #region Properties

    private TValue? Value { get; set; }
    
    [MemberNotNullWhen(true, nameof(Value))]
    public override bool IsSuccess => base.IsSuccess;

    [MemberNotNullWhen(false, nameof(Value))]
    public override bool IsFailure => base.IsFailure;

    #endregion
    
    #region Behaviors

    // Static Factory Methods
    public static Result<TValue> Ok(TValue value) => new Result<TValue>().AddReason(EchoSuccess.Ok()).SetValue(value);
    
    public static Result<TValue> Fail(IEnumerable<IEchoError> errors) => new Result<TValue>().AddReasons(errors);
    public static Result<TValue> Fail(Exception exception) => new Result<TValue>().AddReason(new EchoExceptionalError(exception));

    //----------------------------------------------------------------
    
    public TReturn Match<TReturn>(Func<TValue, TReturn> onSuccess, Func<IEnumerable<IEchoError>, TReturn> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Errors);
    }
    public async Task<TReturn> MatchAsync<TReturn>(Func<TValue, Task<TReturn>> onSuccess, Func<IEnumerable<IEchoError>, Task<TReturn>> onFailure)
    {
        return IsSuccess ? await onSuccess(Value) : await onFailure(Errors);
    }
    public async Task<TReturn> MatchAsync<TReturn>(Func<TValue, Task<TReturn>> onSuccess, Func<IEnumerable<IEchoError>, TReturn> onFailure)
    {
        return IsSuccess ? await onSuccess(Value) : onFailure(Errors);
    }
    
    public bool IsFail([NotNullWhen(false)] out TValue? value, out List<IEchoError> errors)
    {
        if (IsFailure)
        {
            value = default;
            errors = Errors.ToList();
            return true;
        }
        
        value = Value;
        errors = [];
        return false;
    }

    public async Task WhenSuccessAsync(Func<TValue, Task> onSuccess)
    {
        if (IsSuccess) await onSuccess(Value);
    }
    
    private Result<TValue> SetValue(TValue value)
    {
        Value = value;
        return this;
    }

    #endregion
    
    #region Implicit Operators
    
    // Cast Error to Result<T> 
    public static implicit operator Result<TValue>(Exception exception) => Fail(exception);
    public static implicit operator Result<TValue>(List<IEchoError> errors) => Fail(errors);
    public static implicit operator Result<TValue>(IEchoError[] errors) => Fail(errors);
    
    // Cast Success to Result<T>? Think about it
    public static implicit operator Result<TValue>(TValue value) => Ok(value);
    
    // Cast from Result<T> to Result
    public static implicit operator Result(Result<TValue> result) => new Result().AddReasons(result.Reasons);

    #endregion
}