using GeekSevenLabs.AdventEcho.Kernel.Messages;

namespace GeekSevenLabs.AdventEcho.Kernel.Communication.Mediator;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    public async Task PublishEvent(IntegrationEvent integrationEvent) => await mediator.Publish(integrationEvent);
    public async Task PublishEvent(DomainEvent domainEvent) => await mediator.Publish(domainEvent);

    public async Task<Result> SendCommand(ICommand command) => await mediator.Send(command);
    public async Task<Result<TResponse>> SendCommand<TResponse>(ICommand<TResponse> command) => await mediator.Send(command);
}