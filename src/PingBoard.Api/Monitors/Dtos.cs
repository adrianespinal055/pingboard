namespace PingBoard.Api.Monitors;

public sealed record CreateMonitorRequest(
    string Name,string Url,int IntervalSeconds);

public sealed record MonitorResponse(
    Guid Id,
    string Name,
    string Url,
    int IntervalSeconds,
    bool IsActive,
    MonitorStatus CurrentStatus,
    DateTimeOffset CreatedAt)
{
    public static MonitorResponse From(Monitor monitor) => new(
        monitor.Id,
        monitor.Name,
        monitor.Url,
        monitor.IntervalSeconds,
        monitor.IsActive,
        monitor.CurrentStatus,
        monitor.CreatedAt
    );
}


