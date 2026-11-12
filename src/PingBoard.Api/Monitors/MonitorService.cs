namespace PingBoard.Api.Monitors;

public sealed class MonitorService(IMonitorRepository repository,TimeProvider timeProvider)
{
    public MonitorResponse Create(CreateMonitorRequest request)
    {
        var monitor = Monitor.Create(request.Name, request.Url, request.IntervalSeconds, timeProvider.GetUtcNow());
        repository.Add(monitor);
        return MonitorResponse.From(monitor);
    }
    
    
    public IReadOnlyList<MonitorResponse> GetAll()=> repository.GetAll().Select(MonitorResponse.From).ToList();

    public MonitorResponse? GetById(Guid id)
    {
        var monitor = repository.GetById(id);
        return monitor is null ? null : MonitorResponse.From(monitor);
    }
    
}