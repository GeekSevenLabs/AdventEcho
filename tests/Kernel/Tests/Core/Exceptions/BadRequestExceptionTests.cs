namespace AdventEcho.Kernel.Tests.Core.Exceptions;

public class BadRequestExceptionTests
{
    [Fact]
    public void ShouldCreateBadRequestExceptionWithMessage()
    {
        // Arrange
        const string message = "This is a bad request";

        // Act
        var exception = new BadRequestException(message);

        // Assert
        Assert.Equal(message, exception.Message);
    }
}