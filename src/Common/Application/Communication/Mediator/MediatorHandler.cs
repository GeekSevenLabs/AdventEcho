using AdventEcho.Kernel.Application.Shared.Messages;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
using MediatR;

namespace AdventEcho.Kernel.Application.Communication.Mediator;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    public async Task PublishEventAsync(IntegrationEvent integrationEvent) => await mediator.Publish(integrationEvent);
    public async Task PublishEventAsync(DomainEvent domainEvent) => await mediator.Publish(domainEvent);

    public async Task<Result> SendCommandAsync(ICommand command) => await mediator.Send(command);
    public async Task<Result<TResponse>> SendCommandAsync<TResponse>(ICommand<TResponse> command) => await mediator.Send(command);
    public async Task<Result<TResponse>> SendQueryAsync<TResponse>(IQuery<TResponse> queryCommand) => await mediator.Send(queryCommand);
}