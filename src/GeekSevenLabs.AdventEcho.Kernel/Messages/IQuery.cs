namespace GeekSevenLabs.AdventEcho.Kernel.Messages;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;