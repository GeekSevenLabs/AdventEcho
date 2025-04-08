using MediatR;

namespace AdventEcho.Kernel.Application.Shared.Messages;

public interface ICommand : IRequest<Result>;
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;