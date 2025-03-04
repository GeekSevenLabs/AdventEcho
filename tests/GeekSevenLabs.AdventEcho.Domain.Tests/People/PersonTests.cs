using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Domain.Tests.People;

public class PersonTests
{
    [Fact]
    public void ShouldReturnErrorWhenNameIsNull()
    {
        // Arrange
        var contact = new ContactVo("email@domain.com", "12345678900");
        var churchId = ChurchId.New();

        // Act
        var exception = Assert.Throws<DomainException>(() => new Person(null!, contact, churchId));

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == PersonValidationContract.NameIsRequiredMessage);
    }
    
    [Fact]
    public void ShouldReturnErrorWhenContactIsNull()
    {
        // Arrange
        var name = new NameVo("Jhon", "Doe");
        var churchId = ChurchId.New();

        // Act
        var exception = Assert.Throws<DomainException>(() => new Person(name, null!, churchId));

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == PersonValidationContract.ContactIsRequiredMessage);
    }
    
    [Fact]
    public void ShouldReturnErrorWhenChurchIdIsEmpty()
    {
        // Arrange
        var name = new NameVo("Jhon", "Doe");
        var contact = new ContactVo("email@domain.com", "12345678900");
        var churchId = ChurchId.Empty;

        // Act
        var exception = Assert.Throws<DomainException>(() => new Person(name, contact, churchId));

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == PersonValidationContract.ChurchIdIsRequiredMessage);
    }


    [Fact]
    public void ShouldReturnSuccessWhenPersonIsValid()
    {
        // Arrange 
        var name = new NameVo("Jhon", "Doe");
        var contact = new ContactVo("email@domain.com", "12345678900");
        var churchId = ChurchId.New();

        // Act
        var person = new Person(name, contact, churchId);

        // Assert
        Assert.True(person.IsValid);
    }
    
}