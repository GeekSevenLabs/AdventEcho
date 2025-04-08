namespace AdventEcho.Tools.Results.Tests.Reasons;

public class EchoSuccessTests
{
    [Fact]
    public void ShouldCreateEchoSuccessWithEmptyMessage()
    {
        // Arrange
        
        // Act
        var echoSuccess = new EchoSuccess();

        // Assert
        Assert.Equal(string.Empty, echoSuccess.Message);
    }
    
    [Fact]
    public void ShouldCreateEchoSuccessWithMessage()
    {
        // Arrange
        const string message = "Test message";

        // Act
        var echoSuccess = new EchoSuccess(message);

        // Assert
        Assert.Equal(message, echoSuccess.Message);
    }

    [Fact]
    public void ShouldCreateEchoSuccessWithMetadata()
    {
        // Arrange
        const string key = "TestKey";
        const string value = "TestValue";
        
        // Act
        var echoSuccess = new EchoSuccess().WithMetadata(key, value);
        
        // Assert
        Assert.Contains(key, echoSuccess.Metadata.Keys);
        Assert.Equal(value, echoSuccess.Metadata[key]);
    }
    
    [Fact]
    public void ShouldCreateEchoSuccessWithStaticFactoryMethod()
    {
        // Arrange
        
        // Act
        var echoSuccess = EchoSuccess.Ok();
        
        // Assert
        Assert.Equal(string.Empty, echoSuccess.Message);
    }
    
    [Fact]
    public void ShouldCreateEchoSuccessWithStaticFactoryMethodAndMessage()
    {
        // Arrange
        const string message = "Test message";
        
        // Act
        var echoSuccess = EchoSuccess.Ok(message);
        
        // Assert
        Assert.Equal(message, echoSuccess.Message);
    }
    
}