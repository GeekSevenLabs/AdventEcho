using GeekSevenLabs.AdventEcho.Kernel.Messages;

namespace GeekSevenLabs.AdventEcho.Kernel.Communication.Mediator;

public interface IMediatorHandler
{
    // Events
    Task PublishEvent(IntegrationEvent integrationEvent);
    Task PublishEvent(DomainEvent domainEvent);
    
    // Commands
    Task<Result> SendCommand(ICommand command);
    Task<Result<TResponse>> SendCommand<TResponse>(ICommand<TResponse> command);
    
    // Queries
    Task<Result<TResponse>> SendQuery<TResponse>(IQuery<TResponse> queryCommand);
    
    // Thinking about adding a method to publish notifications
}