namespace AdventEcho.Kernel.Tests.Application.Shared.Errors;

public class JsonErrorsTests
{
    [Fact]
    public void InvalidJson_ShouldReturnCorrectError()
    {
        // Arrange
        var expectedError = new EchoError("Json content is null or not valid for this type JsonErrorsTests").WithErrorCode("JSON001");

        // Act
        var result = JsonErrors.InvalidJson<JsonErrorsTests>();

        // Assert
        Assert.Single(result);
        Assert.Equivalent(expectedError, result[0]);
    }
}