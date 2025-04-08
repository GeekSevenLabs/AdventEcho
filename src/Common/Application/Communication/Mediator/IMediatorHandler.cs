using AdventEcho.Kernel.Application.Shared.Messages;

namespace AdventEcho.Kernel.Application.Communication.Mediator;

public interface IMediatorHandler
{
    // Events
    Task PublishEventAsync(IntegrationEvent integrationEvent);
    Task PublishEventAsync(DomainEvent domainEvent);
    
    // Commands
    Task<Result> SendCommandAsync(ICommand command);
    Task<Result<TResponse>> SendCommandAsync<TResponse>(ICommand<TResponse> command);
    
    // Queries
    Task<Result<TResponse>> SendQueryAsync<TResponse>(IQuery<TResponse> queryCommand);
    
    // Thinking about adding a method to publish notifications
}