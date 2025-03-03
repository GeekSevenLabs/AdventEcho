using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Domain.Tests.People;

public class ContactTests
{
    [Fact]
    public void ShouldReturnNotificationWhenEmailIsEmpty()
    {
        // Arrange
        const string email = "";
        const string phone = "82912341234";

        // Act
        var contact = new ContactVo(email, phone);

        // Assert
        Assert.False(contact.IsValid);
        Assert.Contains(contact.Notifications, notification => notification.Message == ContactVoValidationContract.EmailRequiredMessage);
    } 
    
    [Fact]
    public void ShouldReturnNotificationWhenEmailIsNull()
    {
        // Arrange
        string email = null!;
        const string phone = "82912341234";

        // Act
        var contact = new ContactVo(email, phone);

        // Assert
        Assert.False(contact.IsValid);
        Assert.Contains(contact.Notifications, notification => notification.Message == ContactVoValidationContract.EmailRequiredMessage);
    }
    
    [Theory]
    [InlineData("email")]
    [InlineData("email@")]
    [InlineData("email.com")]
    [InlineData("email.com.br")]
    [InlineData("email@.com")]
    public void ShouldReturnNotificationWhenEmailIsInvalid(string email)
    {
        // Arrange
        const string phone = "82912341234";

        // Act
        var contact = new ContactVo(email, phone);

        // Assert
        Assert.False(contact.IsValid);
        Assert.Contains(contact.Notifications, notification => notification.Message == ContactVoValidationContract.EmailInvalidMessage);
    } 
    
    [Fact]
    public void ShouldReturnNotificationWhenPhoneIsEmpty()
    {
        // Arrange
        const string email = "email@email.com";
        const string phone = "";

        // Act
        var contact = new ContactVo(email, phone);

        // Assert
        Assert.False(contact.IsValid);
        Assert.Contains(contact.Notifications, notification => notification.Message == ContactVoValidationContract.PhoneRequiredMessage);
    } 
    
    [Fact]
    public void ShouldReturnNotificationWhenPhoneIsNull()
    {
        // Arrange
        const string email = "email@email.com";
        string phone = null!;

        // Act
        var contact = new ContactVo(email, phone);

        // Assert
        Assert.False(contact.IsValid);
        Assert.Contains(contact.Notifications, notification => notification.Message == ContactVoValidationContract.PhoneRequiredMessage);
    }
    
    [Theory]
    [InlineData("12345678")]
    [InlineData("123456789")]
    [InlineData("1234567890")]
    [InlineData("1234567890A")]
    [InlineData("123.5678901")]
    [InlineData("(82) 9 9999-9999")]
    public void ShouldReturnNotificationWhenPhoneIsInvalid(string phone)
    {
        // Arrange
        const string email = "email@email.com";

        // Act
        var contact = new ContactVo(email, phone);

        // Assert
        Assert.False(contact.IsValid);
        Assert.Contains(contact.Notifications, notification => notification.Message == ContactVoValidationContract.PhoneInvalidMessage);
    }

    [Fact]
    public void ShouldNotReturnNotificationWhenContactIsValid()
    {
        // Arrange
        const string email = "menso@menso.dev.br";
        const string phone = "82912341234";
        
        // Act
        var contact = new ContactVo(email, phone);
        
        // Assert
        Assert.True(contact.IsValid);
    }
}