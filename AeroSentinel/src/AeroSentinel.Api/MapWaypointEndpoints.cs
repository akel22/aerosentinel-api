namespace AeroSentinel.Api
{
    public static class MapWaypointEndpoints
    {
        public static void MapHttpWaypointEndpoints(this WebApplication app)
        {
            var waypoints = app.MapGroup("/waypoints");

            const string routeName = "GetWaypoints";

            // GET ALL
            waypoints.MapGet("/", async (
                [FromServices] IWaypointRepository waypointRepository,
                CancellationToken cancellationToken) =>
            {
                var waypointRecords = await waypointRepository.GetAllWaypointsAsync(cancellationToken);

                return Results.Ok(waypointRecords);
            });
               


             waypoints.MapGet("/{waypointId}", async (string waypointId,
                [FromServices] IWaypointRepository waypointRepository,
                CancellationToken cancellationToken) =>
            {
                var waypoint = await waypointRepository.GetByWaypointIdAsync(waypointId, cancellationToken);

                return waypoint is null
                    ? Results.NotFound(new { error = $"Waypoint '{waypointId}' was not found." })
                    : Results.Ok(waypoint);
            })
            .WithName(routeName);
        }
    }
}