using AdventEcho.Kernel.Application.Shared.Messages.Results;

namespace AdventEcho.Presentation.Web.Client.Services.Results;

public static class TaskExtensions
{
    public static TaskWrapper<T> Use<T>(this Task<Result<T>> task, IUiUtils utils) => new(task, utils);
    public static TaskWrapper Use(this Task<Result> task, IUiUtils utils) => new(task, utils);
}