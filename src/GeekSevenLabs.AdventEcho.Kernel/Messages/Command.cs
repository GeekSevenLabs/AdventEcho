namespace GeekSevenLabs.AdventEcho.Kernel.Messages;

public interface ICommand : IRequest<Result>;
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;