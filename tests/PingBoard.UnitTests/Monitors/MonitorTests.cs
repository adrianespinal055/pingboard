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
}