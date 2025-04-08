using System.Runtime.CompilerServices;

namespace AdventEcho.Presentation.Web.Client.Services.Results;

public abstract class TaskWrapperBase<TChild>(IUiUtils ui) : ITaskWrapperBase<TChild> where TChild : class, ITaskWrapperBase<TChild>
{
    private bool _showError;
    private bool _redirectToLogin;
    private bool _showSuccess;
    private string? _successMessage;
    private bool _showBusy;
    private string? _busyMessage;

    public TChild ShowBusy(string? busyMessage = null)
    {
        _showBusy = true;
        _busyMessage = busyMessage;
        return (this as TChild)!;
    }

    public TChild ShowSuccess(string? successMessage = null)
    {
        _showSuccess = true;
        _successMessage = successMessage;
        return (this as TChild)!;
    }

    public TChild ShowError(bool redirectToLogin = true)
    {
        _showError = true;
        _redirectToLogin = redirectToLogin;
        return (this as TChild)!;
    }
    
    
    protected async Task<Result> Execute(Task<Result> task)
    {
        Guid? busyId = null;
        try
        {
            busyId = await TryShowBusyAsync();
            
            var result = await task;

            await result.WhenSuccessAsync(TryShowSuccessAsync);
            await result.WhenFailureAsync(TryShowErrorOrRedirectToLoginPageAsync);
            
            return result;
        }
        catch (Exception ex)
        {
            await TryShowErrorOrRedirectToLoginPageAsync([new EchoExceptionalError(ex)]);
            
            // Think about logging the exception here or not rethrowing it
            throw;
        }
        finally
        {
            await TryHideBusyAsync(busyId.GetValueOrDefault());
        }
    }
    
    protected async Task<Result<T>> Execute<T>(Task<Result<T>> task)
    {
        Guid? busyId = null;
        try
        {
            busyId = await TryShowBusyAsync();
            
            var value = await task;

            await value.WhenSuccessAsync(TryShowSuccessAsync);
            await value.WhenFailureAsync(TryShowErrorOrRedirectToLoginPageAsync);
            
            return value;
        }
        catch (Exception ex)
        {
            await TryShowErrorOrRedirectToLoginPageAsync([new EchoExceptionalError(ex)]);
            
            // Think about logging the exception here or not rethrowing it
            throw;
        }
        finally
        {
            await TryHideBusyAsync(busyId.GetValueOrDefault());
        }
    }
    
    private async Task<Guid?> TryShowBusyAsync()
    {
        if (_showBusy)
        {
            return await ui.ShowBusyAsync(_busyMessage ?? "Processing...");
        }
        
        return null;
    }
    
    private async Task TryShowSuccessAsync()
    {
        if (_showSuccess)
        {
            await ui.ShowSuccessAsync(_successMessage ?? "Operation completed successfully.");
        }
    }
    
    private async Task TryShowErrorOrRedirectToLoginPageAsync(IEchoError[] errors)
    {
        if (_showError)
        {
            if(_redirectToLogin && IsUnauthorized(errors)) 
            {
                ui.NavigateTo("/account/login");
            }
            else
            {
                await ui.ShowErrorAsync(errors);
            }
        }
    }

    private static bool IsUnauthorized(IEchoError[] errors)
    {
        if(errors.HasException<UnauthorizedException>()) return true;
        return errors.TryGetException<ProblemDetailsException>(out var problem) && problem?.IsUnauthorized == true;
    } 
    
    private async Task TryHideBusyAsync(Guid busyId)
    {
        if (_showBusy)
        {
            await ui.HideBusyAsync(busyId);
        }
    }
}

public class TaskWrapper(Task<Result> task, IUiUtils ui) : TaskWrapperBase<ITaskWrapper>(ui), ITaskWrapper
{
    public TaskAwaiter<Result> GetAwaiter() => Execute(task).GetAwaiter();
}

public class TaskWrapper<T>(Task<Result<T>> task, IUiUtils ui) : TaskWrapperBase<ITaskWrapper<T>>(ui), ITaskWrapper<T>
{
    public TaskAwaiter<Result<T>> GetAwaiter() => Execute(task).GetAwaiter();
}