using PingBoard.Api.Monitors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

builder.Services.AddSingleton<IMonitorRepository,InMemoryMonitorRepository>();
builder.Services.AddSingleton<MonitorService>();
builder.Services.AddSingleton(TimeProvider.System);



var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "PingBoard API v1");
    });
}

app.MapHealthChecks("/health");
app.MapMonitorEndpoints();
app.Run();

public partial class Program { }
