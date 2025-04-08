// ReSharper disable once CheckNamespace
namespace AdventEcho;

public interface ICommand : IRequest<Result>;
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;