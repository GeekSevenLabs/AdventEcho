namespace AdventEcho.Kernel.Tests.Application.Errors;

public class SecurityErrorsTests
{
    [Fact]
    public void Unauthorized_ShouldReturnCorrectError()
    {
        // Arrange
        var expectedError = new EchoError("Unauthorized").WithErrorCode("SCY0001");

        // Act
        var result = SecurityErrors.Unauthorized;

        // Assert
        Assert.Single(result);
        Assert.Equivalent(expectedError, result[0]);
    }
    
    [Fact]
    public void Forbidden_ShouldReturnCorrectError()
    {
        // Arrange
        const string message = "Access denied";
        var expectedError = new EchoError(message).WithErrorCode("SCY0002");

        // Act
        var result = SecurityErrors.Forbidden(message);

        // Assert
        Assert.Single(result);
        Assert.Equivalent(expectedError, result[0]);
    }
}