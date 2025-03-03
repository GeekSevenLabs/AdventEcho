using GeekSevenLabs.AdventEcho.Domain.Noticies;

namespace GeekSevenLabs.AdventEcho.Domain.Tests.Noticies;

public class PeriodVoTests
{
    [Fact]
    public void ShouldReturnNotificationWhenStartInIsGreaterThanEndIn()
    {
        // Arrange
        var startIn = DateTimeOffset.Now.AddDays(1);
        var endIn = DateTimeOffset.Now.AddDays(-1);

        // Act
        var contact = new PeriodVo(startIn, endIn);

        // Assert
        Assert.False(contact.IsValid);
        Assert.Contains(contact.Notifications, notification => notification.Message == PeriodVoValidationContract.StartShouldBeLowerThanEndMessage);
    } 
    
    [Fact]
    public void ShouldDoesNotReturnNotificationWhenStartInIsLessThanEndIn()
    {
        // Arrange
        var startIn = DateTimeOffset.Now.AddDays(-1);
        var endIn = DateTimeOffset.Now.AddDays(+1);

        // Act
        var contact = new PeriodVo(startIn, endIn);

        // Assert
        Assert.True(contact.IsValid);
        Assert.DoesNotContain(contact.Notifications, notification => notification.Message == PeriodVoValidationContract.StartShouldBeLowerThanEndMessage);
        Assert.DoesNotContain(contact.Notifications, notification => notification.Message == PeriodVoValidationContract.EndInIsRequiredMessage);
        Assert.DoesNotContain(contact.Notifications, notification => notification.Message == PeriodVoValidationContract.StartInIsRequiredMessage);
    } 
}