using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Domain.Tests.People;

public class NameVoTests
{
    [Fact]
    public void ShouldReturnNotificationWhenFirstIsEmpty()
    {
        // Arrange
        const string first = "";
        const string last = "Last";

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.FirstNameIsRequired);
    }

    [Fact]
    public void ShouldReturnNotificationWhenLastIsEmpty()
    {
        // Arrange
        const string first = "Jhon";
        const string last = "";

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.LastNameIsRequired);
    }

    [Fact]
    public void ShouldReturnNotificationWhenFirstAndLastAreEmpty()
    {
        // Arrange
        const string first = "";
        const string last = "";

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.FirstNameIsRequired);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.LastNameIsRequired);
    }

    [Fact]
    public void ShouldReturnNotificationWhenFirstAndLastAreNull()
    {
        // Arrange
        string first = null!;
        string last = null!;

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.FirstNameIsRequired);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.LastNameIsRequired);
    }

    [Fact]
    public void ShouldReturnNotificationWhenFirstIsNull()
    {
        // Arrange
        string first = null!;
        const string last = "Doe";

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.FirstNameIsRequired);
    }

    [Fact]
    public void ShouldReturnNotificationWhenLastIsNull()
    {
        // Arrange
        const string first = "John";
        string last = null!;

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.LastNameIsRequired);
    }
    

    [Fact]
    public void ShouldReturnNotificationWhenFirstIsLassThenThree()
    {
        // Arrange
        const string first = "JJ";
        const string last = "Doe Jhon";

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.FirstNameMinLength);
    }
    
    [Fact]
    public void ShouldReturnNotificationWhenFirstIsGreaterThenFifty()
    {
        // Arrange
        const string first = "Jhoooooooooooooooooooooooooooooooooooooooooooooooon";
        const string last = "Doe Jhon";

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.FirstNameMaxLength);
    }
    
    [Fact]
    public void ShouldReturnNotificationWhenLastIsLassThenThree()
    {
        // Arrange
        const string first = "Doe Jhon";
        const string last = "jj";

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.LastNameMinLength);
    }
    
    [Fact]
    public void ShouldReturnNotificationWhenLastIsGreaterThenHundred()
    {
        // Arrange
        const string first = "Doe Jhon";
        const string last = "Jhoooooooooooooooooooooooooooooooooooooooooooooooon Jhoooooooooooooooooooooooooooooooooooooooooooooooon";

        // Act
        var name = new NameVo(first, last);

        // Assert
        Assert.False(name.IsValid);
        Assert.Contains(name.Notifications, notification => notification.Message == NameVoValidationContract.LastNameMaxLength);
    }
}