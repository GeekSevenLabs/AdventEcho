using AdventEcho.Kernel.Application.Shared.Messages.Results;
using MediatR;

namespace AdventEcho.Kernel.Application.Shared.Messages;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;