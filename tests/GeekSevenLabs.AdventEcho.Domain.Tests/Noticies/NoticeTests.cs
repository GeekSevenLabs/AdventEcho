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
        var districtId = Guid.NewGuid();
        Guid? churchId = null;

        // Act
        var exception = Assert.Throws<DomainException>(
            () => new Notice(title, description, period, notifyEveryDay, districtId, churchId)
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
        var districtId = Guid.NewGuid();
        Guid? churchId = null;

        // Act
        var exception = Assert.Throws<DomainException>(
            () => new Notice(title, description, period, notifyEveryDay, districtId, churchId)
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
        var districtId = Guid.NewGuid();
        Guid? churchId = null;

        // Act
        var exception = Assert.Throws<DomainException>(
            () => new Notice(title, description, null!, notifyEveryDay, districtId, churchId)
        );

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == NoticeValidationContract.PeriodIsRequired);
    }
    
    [Fact]
    public void ShouldReturnErrorWhenDistrictIdAndChurchIdBothAreNull()
    {
        // Arrange
        const string title = "";
        const string description = "";
        var period = new PeriodVo(DateTime.Now, DateTime.Now.AddDays(10));
        const bool notifyEveryDay = false;
        Guid? districtId = null;
        Guid? churchId = null;

        // Act
        var exception = Assert.Throws<DomainException>(
            () => new Notice(title, description, period, notifyEveryDay, districtId, churchId)
        );

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == NoticeValidationContract.DistrictIdOrChurchIdIsRequired);
    }
    
    [Fact]
    public void ShouldReturnErrorWhenDistrictIdAndChurchIdBothAreNotNull()
    {
        // Arrange
        const string title = "";
        const string description = "";
        var period = new PeriodVo(DateTime.Now, DateTime.Now.AddDays(10));
        const bool notifyEveryDay = false;
        Guid? districtId = Guid.NewGuid();
        Guid? churchId = Guid.NewGuid();

        // Act
        var exception = Assert.Throws<DomainException>(
            () => new Notice(title, description, period, notifyEveryDay, districtId, churchId)
        );

        // Assert
        Assert.Contains(exception.Notifications, notification => notification.Message == NoticeValidationContract.DistrictIdOrChurchIdNotBoth);
    }
    
    [Fact]
    public void ShouldDoesNotReturnErrorWhenNoticeIsValid()
    {
        // Arrange
        const string title = "Notice";
        const string description = "Notice description";
        var period = new PeriodVo(DateTime.Now, DateTime.Now.AddDays(10));
        const bool notifyEveryDay = false;
        Guid? districtId = Guid.NewGuid();
        Guid? churchId = null;

        // Act
        var notice = new Notice(title, description, period, notifyEveryDay, districtId, churchId);

        // Assert
        Assert.True(notice.IsValid);
    }
}