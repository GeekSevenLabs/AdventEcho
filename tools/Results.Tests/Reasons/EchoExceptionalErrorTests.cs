namespace AdventEcho.Tools.Results.Tests.Reasons;

public class EchoExceptionalErrorTests
{
    [Fact]
    public void ShouldCreateEchoExceptionalErrorWithException()
    {
        // Arrange
        var exception = new Exception("Test exception");
        
        // Act
        var error = new EchoExceptionalError(exception);
        
        // Assert
        Assert.Equal(exception, error.Exception);
    }
    
    [Fact]
    public void ShouldCreateEchoExceptionalErrorWithMessageAndException()
    {
        // Arrange
        const string message = "An error occurred";
        var exception = new Exception("Test exception");
        
        // Act
        var error = new EchoExceptionalError(message, exception);
        
        // Assert
        Assert.Equal(message, error.Message);
        Assert.Equal(exception, error.Exception);
    }
}