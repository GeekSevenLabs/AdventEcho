// ReSharper disable once CheckNamespace
namespace AdventEcho;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;