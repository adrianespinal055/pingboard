using Monitor = PingBoard.Api.Monitors.Monitor;

using PingBoard.Api.Monitors;

namespace PingBoard.UnitTests.Monitors;

public class MonitorTests
{
    private static readonly DateTimeOffset Now = new(2026, 11, 9, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Create_WithValidData_ReturnsActiveMonitorWithUnknownStatus()
    {
        // Arrange
        var name = "My blog";
        var url = "https://myblog.com";
        var interval = 60;

        // Act
        var monitor = Monitor.Create(name, url, interval, Now);

        // Assert
        Assert.NotEqual(Guid.Empty, monitor.Id);
        Assert.Equal(name, monitor.Name);
        Assert.Equal(url, monitor.Url);
        Assert.Equal(interval, monitor.IntervalSeconds);
        Assert.True(monitor.IsActive);
        Assert.Equal(MonitorStatus.Unknown, monitor.CurrentStatus);
        Assert.Equal(Now, monitor.CreatedAt);
    }

    [Theory]
    [InlineData(59)]
    [InlineData(3601)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_WithIntervalOutOfRange_Throws(int interval)
    {
        Assert.Throws<ArgumentException>(() => Monitor.Create("My blog", "https://myblog.com", interval, Now));

    }
    [Theory]
    [InlineData("ftp://site.com")]
    [InlineData("not a url")]
    [InlineData("/relative/path")]
    public void Create_WithInvalidUrl_Throws(string url)
    {
        Assert.Throws<ArgumentException>(() => Monitor.Create("My blog", url, 60, Now));
    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    public void Create_WithEmptyName_Throws(string name)
    {
        Assert.Throws<ArgumentException>(() => Monitor.Create(name, "https://myblog.com", 60, Now));
    }

    [Fact]
    public void Create_WithNameMoreThan100Characters_Throws()
    {
        var longName = new string('a', 101);
        Assert.Throws<ArgumentException>(() => Monitor.Create(longName, "https://myblog.com", 60, Now));
    }
}