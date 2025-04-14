// ReSharper disable once CheckNamespace
namespace AdventEcho;

public interface IMediatorHandler
{
    // Events
    Task PublishEventAsync(IntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
    Task PublishEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default);
    
    // Commands
    Task<Result> SendCommandAsync(ICommand command, CancellationToken cancellationToken = default);
    Task<Result<TResponse>> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    
    // Queries
    Task<Result<TResponse>> SendQueryAsync<TResponse>(IQuery<TResponse> queryCommand, CancellationToken cancellationToken = default);
    
    // Thinking about adding a method to publish notifications
}