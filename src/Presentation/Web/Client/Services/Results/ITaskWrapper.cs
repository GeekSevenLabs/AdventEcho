using System.Runtime.CompilerServices;

namespace AdventEcho.Presentation.Web.Client.Services.Results;

public interface ITaskWrapperBase<out TChild> where TChild : ITaskWrapperBase<TChild>
{
    TChild ShowBusy(string? busyMessage = null);
    /// <summary>
    /// Show error message when Result is not success or when exception is thrown.
    /// </summary>
    /// <param name="redirectToLogin">
    /// When true and Unauthorized exception is thrown, redirect to login page.
    /// </param>
    /// <returns></returns>
    TChild ShowError(bool redirectToLogin = true);
    TChild ShowSuccess(string? successMessage = null);
}

public interface ITaskWrapper<T> : ITaskWrapperBase<ITaskWrapper<T>>
{
    TaskAwaiter<Result<T>> GetAwaiter();
}

public interface ITaskWrapper : ITaskWrapperBase<ITaskWrapper>
{
    TaskAwaiter<Result> GetAwaiter();
}