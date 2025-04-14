namespace AdventEcho.Tools.Results.Tests.Reasons;

public class EchoErrorTests
{
    [Fact]
    public void ShouldCreateEchoErrorWithMessage()
    {
        // Arrange
        const string message = "An error occurred";
        
        // Act
        var error = new EchoError(message);
        
        // Assert
        Assert.Equal(message, error.Message);
    }

    [Fact]
    public void ShouldCreateEchoErrorWithMessageAndMetadata()
    {
        // Arrange
        const string message = "An error occurred";
        const string metadataKey = "key";
        const string metadataValue = "value";
        
        // Act
        var error = new EchoError(message).WithMetadata(metadataKey, metadataValue);
        
        // Assert
        Assert.Equal(message, error.Message);
        Assert.Contains(metadataKey, error.Metadata.Keys);
        Assert.Equal(metadataValue, error.Metadata[metadataKey]);
    }
    
    [Fact]
    public void ShouldCreateEchoErrorWithMessageAndErrorCode()
    {
        // Arrange
        const string message = "An error occurred";
        const string errorCode = "ERROR_CODE";
        
        // Act
        var error = new EchoError(message).WithErrorCode(errorCode);
        
        // Assert
        Assert.Equal(message, error.Message);
        Assert.Contains(ResultConstants.Metadata.ErrorCode, error.Metadata.Keys);
        Assert.Equal(errorCode, error.Metadata[ResultConstants.Metadata.ErrorCode]);
    }
}