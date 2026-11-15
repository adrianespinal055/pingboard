namespace PingBoard.Api.Monitors;

public interface IMonitorRepository
{
    void Add(Monitor monitor);
    IReadOnlyList<Monitor> GetAll();
    Monitor? GetById(Guid id);

}