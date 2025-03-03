using GeekSevenLabs.AdventEcho.Domain.Districts;

namespace GeekSevenLabs.AdventEcho.Domain.Tests.Districts;

public class DistrictTests
{
    [Fact]
    public void ShouldCreateValidDistrict()
    {
        // Arrange
        const string name = "District 1";
        var pastorId = Guid.NewGuid();

        // Act
        var district = new District(name, pastorId);

        // Assert
        Assert.NotNull(district);
    }
    
    [Fact]
    public void ShouldNotCreateDistrictWithoutName()
    {
        // Arrange
        const string name = "";
        var pastorId = Guid.NewGuid();

        // Act
        var exception = Assert.Throws<DomainException>(() => new District(name, pastorId));

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == DistrictValidationContract.NameRequiredMessage);
    }
    
    [Fact]
    public void ShouldNotCreateDistrictWithoutPastorId()
    {
        // Arrange
        const string name = "District 1";
        var pastorId = Guid.Empty;

        // Act
        var exception = Assert.Throws<DomainException>(() => new District(name, pastorId));

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == DistrictValidationContract.PastorIdRequiredMessage);
    }
    
    [Fact]
    public void ShouldNotCreateDistrictWithoutNameAndPastorId()
    {
        // Arrange
        const string name = "";
        var pastorId = Guid.Empty;

        // Act
        var exception = Assert.Throws<DomainException>(() => new District(name, pastorId));

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == DistrictValidationContract.NameRequiredMessage);
        Assert.Contains(exception.Notifications, notification => notification.Message == DistrictValidationContract.PastorIdRequiredMessage);
    }
    
    [Fact]
    public void ShouldUpdateDistrict()
    {
        // Arrange
        const string name = "District 1";
        var pastorId = Guid.NewGuid();
        
        var district = new District(name, pastorId);
        
        const string newName = "District 2";
        var newPastorId = Guid.NewGuid();

        // Act
        district.Update(newName, newPastorId);

        // Assert
        Assert.Equal(newName, district.Name);
        Assert.Equal(newPastorId, district.PastorId);
    }
}