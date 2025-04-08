namespace AdventEcho.Kernel.Tests.Core.Exceptions;

public class ProblemDetailsExceptionTests
{
    [Fact]
    public void ShouldCreteProblemDetailsExceptionTests()
    {
        // Arrange
        const string type = "https://example.com/problem-type";
        const string title = "Problem Title";
        const int status = 400;
        const string detail = "Detailed description of the problem";
        var errors = new Dictionary<string, string[]>
        {
            { "field1", ["Error message 1"] },
            { "field2", ["Error message 2", "Error message 3"] }
        };
        
        // Act
        var exception = new ProblemDetailsException(type, title, status, detail, errors);
        
        // Assert
        Assert.Equal(type, exception.Type);
        Assert.Equal(title, exception.Title);
        Assert.Equal(status, exception.Status);
    }
    
    [Fact]
    public void ShouldBeTrueForUnauthorizedStatus()
    {
        // Arrange
        const string type = "https://example.com/problem-type";
        const string title = "Problem Title";
        const int status = 401;
        const string detail = "Detailed description of the problem";
        var errors = new Dictionary<string, string[]>();
        
        // Act
        var exception = new ProblemDetailsException(type, title, status, detail, errors);
        
        // Assert
        Assert.True(exception.IsUnauthorized);
    }
}