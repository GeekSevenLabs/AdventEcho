using AdventEcho.Kernel.Application.Shared.Messages;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
using MediatR;

namespace AdventEcho.Kernel.Application.Handlers;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>;