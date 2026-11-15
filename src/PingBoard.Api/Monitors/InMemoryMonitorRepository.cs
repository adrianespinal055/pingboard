using System.Collections.Concurrent;

namespace PingBoard.Api.Monitors;

public sealed class InMemoryMonitorRepository:IMonitorRepository
{
    private readonly ConcurrentDictionary<Guid, Monitor> _monitors = new();

    public void Add(Monitor monitor)=>_monitors[monitor.Id] = monitor;

    public IReadOnlyList<Monitor> GetAll()=> _monitors.Values.OrderBy(m => m.CreatedAt).ToList();

    public Monitor? GetById(Guid id)
    {
         return _monitors.GetValueOrDefault(id);
    }
}