using Microsoft.AspNetCore.Http.HttpResults;

namespace PingBoard.Api.Monitors;

public static class MonitorEndpoints
{

    public static IEndpointRouteBuilder MapMonitorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/monitors").WithTags("Monitors");
        group.MapPost("/", Create);
        group.MapGet("/", GetAll);
        group.MapGet("/{id:Guid}",GetById);
        return app;
    }

    private static Results<Created<MonitorResponse>, ValidationProblem> Create(CreateMonitorRequest request,
        MonitorService service)
    {
        try
        {
            var monitor = service.Create(request);
            return TypedResults.Created($"/api/monitors/{monitor.Id}", monitor);
        }
        catch (ArgumentException ex)
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [ex.ParamName ?? "Request"] = [ex.Message]
            });
        }
    }


    private static Ok<IReadOnlyList<MonitorResponse>> GetAll(MonitorService service)
    {
        return TypedResults.Ok(service.GetAll());
    }

    private static Results<Ok<MonitorResponse>, NotFound> GetById(Guid id,MonitorService service)
    {
        var monitor = service.GetById(id);
        return monitor is null ? TypedResults.NotFound() : TypedResults.Ok(monitor);
    }
}