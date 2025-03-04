using GeekSevenLabs.AdventEcho.Domain.Noticies;

namespace GeekSevenLabs.AdventEcho.Domain.Tests.Noticies;

public class NoticeTests
{
    [Fact]
    public void ShouldReturnErrorWhenTitleIsEmpty()
    {
        // Arrange
        const string title = "";
        const string description = "";
        var period = new PeriodVo(DateTime.Now, DateTime.Now.AddDays(10));
        const bool notifyEveryDay = false;
        var districtId = DistrictId.New();

        // Act
        var exception = Assert.Throws<DomainException>(
            () => new Notice(title, description, period, notifyEveryDay, districtId)
        );

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == NoticeValidationContract.TitleIsRequired);
    }
    
    [Fact]
    public void ShouldReturnErrorWhenDescriptionIsEmpty()
    {
        // Arrange
        const string title = "";
        const string description = "";
        var period = new PeriodVo(DateTime.Now, DateTime.Now.AddDays(10));
        const bool notifyEveryDay = false;
        var districtId = DistrictId.New();

        // Act
        var exception = Assert.Throws<DomainException>(
            () => new Notice(title, description, period, notifyEveryDay, districtId)
        );

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == NoticeValidationContract.DescriptionIsRequired);
    }
    
    [Fact]
    public void ShouldReturnErrorWhenPeriodIsNull()
    {
        // Arrange
        const string title = "";
        const string description = "";
        const bool notifyEveryDay = false;
        var districtId = DistrictId.New();

        // Act
        var exception = Assert.Throws<DomainException>(
            () => new Notice(title, description, null!, notifyEveryDay, districtId)
        );

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == NoticeValidationContract.PeriodIsRequired);
    }
    
    [Fact]
    public void ShouldDoesNotReturnErrorWhenNoticeIsValid()
    {
        // Arrange
        const string title = "Notice";
        const string description = "Notice description";
        var period = new PeriodVo(DateTime.Now, DateTime.Now.AddDays(10));
        const bool notifyEveryDay = false;
        var districtId = DistrictId.New();

        // Act
        var notice = new Notice(title, description, period, notifyEveryDay, districtId);

        // Assert
        Assert.True(notice.IsValid);
        Assert.DoesNotContain(notice.Notifications, notification => notification.Message == NoticeValidationContract.DistrictIdOrChurchIdNotBoth);
        Assert.DoesNotContain(notice.Notifications, notification => notification.Message == NoticeValidationContract.DistrictIdOrChurchIdIsRequired);
    }
}