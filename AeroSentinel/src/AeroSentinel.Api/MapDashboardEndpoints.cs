namespace AeroSentinel.Api;

public static class MapDashboardEndpoints
{
    public static void MapHttpDashboardEndpoints(this WebApplication app)
    {
        app.MapGet("/dashboard", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var snapshot = await sender.Send(new ReadTelemetryCommand(), cancellationToken);
            
            return Results.Ok(snapshot);
        });
    }
}
