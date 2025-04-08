namespace AdventEcho.Kernel.Tests.Core.Exceptions;

public class UnauthorizedExceptionTests
{
    [Fact]
    public void ShouldCreateUnauthorizedExceptionWithMessage()
    {
        // Arrange
        const string message = "This is a bad request";

        // Act
        var exception = new UnauthorizedException(message);

        // Assert
        Assert.Equal(message, exception.Message);
    }
}