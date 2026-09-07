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

            // // GET BY MESSAGE ID
            // waypoints.MapGet("/{waypointId}", async (
            //     Guid messageId,
            //     [FromServices] IWaypointRepository waypointRepository,
            //     CancellationToken cancellationToken) =>
            // {
            //     try
            //     {
            //         var waypoint = await waypointRepository.GetByMessageIdAsync(messageId);

            //         return Results.Ok(telemetry);
            //     }
            //     catch (DomainException exception)
            //     {
            //         return Results.NotFound(new
            //         {
            //             error = exception.Message
            //         });
            //     }
            // })
            // .WithName(routeName);
        }
    }
}