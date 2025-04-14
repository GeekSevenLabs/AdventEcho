// ReSharper disable once CheckNamespace
namespace AdventEcho;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    public async Task PublishEventAsync(IntegrationEvent integrationEvent, CancellationToken cancellationToken = default) 
        => await mediator.Publish(integrationEvent, cancellationToken);
    public async Task PublishEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default) 
        => await mediator.Publish(domainEvent, cancellationToken);

    public async Task<Result> SendCommandAsync(ICommand command, CancellationToken cancellationToken = default) 
        => await mediator.Send(command, cancellationToken);
    public async Task<Result<TResponse>> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default) 
        => await mediator.Send(command, cancellationToken);
    public async Task<Result<TResponse>> SendQueryAsync<TResponse>(IQuery<TResponse> queryCommand, CancellationToken cancellationToken = default) 
        => await mediator.Send(queryCommand, cancellationToken);
}