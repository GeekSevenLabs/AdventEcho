using AdventEcho.Kernel.Application.Shared.Messages;
using MediatR;

namespace AdventEcho.Kernel.Application.Handlers;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>;