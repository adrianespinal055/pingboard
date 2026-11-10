
namespace PingBoard.Api.Monitors;

public sealed class Monitor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public int IntervalSeconds { get; private set; }
    public bool IsActive { get; private set; }
    public MonitorStatus CurrentStatus { get; private set; } = MonitorStatus.Unknown;
    public DateTimeOffset CreatedAt { get; private set; }

    private Monitor() { }

    public static Monitor Create(string name, string url,int intervalSeconds, DateTimeOffset now)
    {
        //throw new NotImplementedException();

        return new Monitor
        {
            Id = Guid.NewGuid(),
            Name = name,
            Url = url,
            IntervalSeconds = intervalSeconds,
            IsActive = true,
            CurrentStatus = MonitorStatus.Unknown,
            CreatedAt = now
        };
    }


}