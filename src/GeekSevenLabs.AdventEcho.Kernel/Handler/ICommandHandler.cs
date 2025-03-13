using GeekSevenLabs.AdventEcho.Kernel.Messages;

namespace GeekSevenLabs.AdventEcho.Kernel.Handler;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>;