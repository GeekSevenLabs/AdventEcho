namespace AdventEcho.Tools.Results.Tests;

public class TypedResultTests
{
    [Fact]
    public void ShouldCreateSuccessResultWithValue()
    {
        // Arrange
        const string value = "Hello, World!";
        
        // Act
        var result = Result<string>.Ok(value);
        
        // Assert
        Assert.Empty(result.Errors);
        Assert.Single(result.Successes);
    }
}