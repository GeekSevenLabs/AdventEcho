using MediatR;
using Moq;

namespace AdventEcho.Kernel.Tests.Application.Communication.Mediator;

public class MediatorHandlerTests
{
    private class TestIntegrationEvent : IntegrationEvent;
    private class TestDomainEvent : DomainEvent;
    private class TestCommand : ICommand;
    private class TestCommandResponse : ICommand<string>;
    private class TestQuery : IQuery<string>;
    
    [Fact]
    public async Task PublishEventAsync_ShouldCallMediatorPublish()
    {
        // Arrange
        var mediator = Mock.Of<IMediator>();
        var mediatorHandler = new MediatorHandler(mediator);
        var integrationEvent = new TestIntegrationEvent();
        
        // Act
        await mediatorHandler.PublishEventAsync(integrationEvent, CancellationToken.None);

        // Assert
        Mock.Get(mediator)
            .Verify(m => m.Publish(It.IsAny<IntegrationEvent>(), CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task PublishEventAsync_ShouldCallMediatorPublishForDomainEvent()
    {
        // Arrange
        var mediator = Mock.Of<IMediator>();
        var mediatorHandler = new MediatorHandler(mediator);
        var domainEvent = new TestDomainEvent();
        
        // Act
        await mediatorHandler.PublishEventAsync(domainEvent, CancellationToken.None);

        // Assert
        Mock.Get(mediator)
            .Verify(m => m.Publish(It.IsAny<DomainEvent>(), CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task SendCommandAsync_ShouldCallMediatorSend()
    {
        // Arrange
        var mediator = Mock.Of<IMediator>();
        var mediatorHandler = new MediatorHandler(mediator);
        var command = new TestCommand();
        
        // Act
        await mediatorHandler.SendCommandAsync(command, CancellationToken.None);

        // Assert
        Mock.Get(mediator)
            .Verify(m => m.Send(It.IsAny<ICommand>(), CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task SendCommandAsync_ShouldCallMediatorSendForCommandResponse()
    {
        // Arrange
        var mediator = Mock.Of<IMediator>();
        var mediatorHandler = new MediatorHandler(mediator);
        var command = new TestCommandResponse();
        
        // Act
        await mediatorHandler.SendCommandAsync(command, CancellationToken.None);

        // Assert
        Mock.Get(mediator)
            .Verify(m => m.Send(It.IsAny<ICommand<string>>(), CancellationToken.None), Times.Once);
    }
    
    [Fact]  
    public async Task SendQueryAsync_ShouldCallMediatorSend()
    {
        // Arrange
        var mediator = Mock.Of<IMediator>();
        var mediatorHandler = new MediatorHandler(mediator);
        var query = new TestQuery();
        
        // Act
        await mediatorHandler.SendQueryAsync(query, CancellationToken.None);

        // Assert
        Mock.Get(mediator)
            .Verify(m => m.Send(It.IsAny<IQuery<string>>(), CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task SendQueryAsync_ShouldReturnResult()
    {
        // Arrange
        var mediator = Mock.Of<IMediator>();
        var mediatorHandler = new MediatorHandler(mediator);
        var query = new TestQuery();
        const string expectedResponse = "response";
        
        Mock.Get(mediator)
            .Setup(m => m.Send(It.IsAny<IQuery<string>>(), CancellationToken.None))
            .ReturnsAsync(expectedResponse);
        
        // Act
        var result = await mediatorHandler.SendQueryAsync(query, CancellationToken.None);

        // Assert
        var value = "";
        result.WhenSuccess(v => value = v);
        
        Assert.Equal(expectedResponse, value);
    }
}