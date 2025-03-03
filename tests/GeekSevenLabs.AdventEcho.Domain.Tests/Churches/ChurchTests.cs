using GeekSevenLabs.AdventEcho.Domain.Churches;

namespace GeekSevenLabs.AdventEcho.Domain.Tests.Churches;

public class ChurchTests
{
    [Fact]
    public void ShouldReturnErrorWhenNameIsEmpty()
    {
        // Arrange
        const string name = "";
        var districtId = Guid.NewGuid();

        // Act
        var exception = Assert.Throws<DomainException>(() => new Church(name, districtId));

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == ChurchValidationContract.NameRequiredMessage);
    }
    
    [Fact]
    public void ShouldReturnErrorWhenNameIsLessThanThreeCharacters()
    {
        // Arrange
        const string name = "Ch";
        var districtId = Guid.NewGuid();

        // Act
        var exception = Assert.Throws<DomainException>(() => new Church(name, districtId));

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == ChurchValidationContract.NameIsLassThanThreeCharactersMessage);
    }
    
    [Fact]
    public void ShouldReturnErrorWhenDistrictIdIsEmpty()
    {
        // Arrange
        const string name = "Church Name";
        var districtId = Guid.Empty;

        // Act
        var exception = Assert.Throws<DomainException>(() => new Church(name, districtId));

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == ChurchValidationContract.DistrictIdRequiredMessage);
    }
    
    [Fact]
    public void ShouldReturnErrorWhenChurchIsInvalid()
    {
        // Arrange
        const string name = "";
        var districtId = Guid.Empty;

        // Act
        var exception = Assert.Throws<DomainException>(() => new Church(name, districtId));

        // Assert
        Assert.Equal(3, exception.Notifications.Length);
    }
    
    [Fact]
    public void ShouldReturnSuccessWhenChurchIsValid()
    {
        // Arrange 
        const string name = "Church Name";
        var districtId = Guid.NewGuid();
        
        // Act
        var church = new Church(name, districtId);
        
        // Assert
        Assert.True(church.IsValid);
    }
}