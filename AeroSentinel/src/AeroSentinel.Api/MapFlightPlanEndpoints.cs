namespace AeroSentinel.Api;

public static class MapFlightPlanEndpoints
{
    public static void MapHttpFlightPlanEndpoints(this WebApplication app)
    {
        var flightPlans = app.MapGroup("/flight-plans");

        flightPlans.MapPost("/", async (
            [FromBody] CreateFlightPlanDTO request,
            [FromServices] ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var flightPlan = new FlightPlan(
                    request.ICAO24,
                    request.Callsign,
                    request.DepartureAirport,
                    request.DestinationAirport,
                    request.DepartureTimeUtc,
                    request.EstimatedArrivalTimeUtc,
                    request.ActualArrivalTimeUtc,
                    request.FlightStatus);

                await dbContext.flight_plan.AddAsync(flightPlan, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                return Results.Created($"/flight-plans/{flightPlan.FlightPlanId}", flightPlan);
            }
            catch (DomainException exception)
            {
                return Results.BadRequest(new { error = exception.Message });
            }
        });

        flightPlans.MapPost("/{flightPlanId:guid}/routes", async (
            Guid flightPlanId,
            [FromBody] AddFlightPlanRouteDTO request,
            [FromServices] ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            if (!await dbContext.flight_plan.AnyAsync(x => x.FlightPlanId == flightPlanId, cancellationToken))
            {
                return Results.NotFound(new { error = "Flight plan was not found." });
            }

            FlightPlanRoute route;
            //BULK INSERT
            try
            {
                route = new FlightPlanRoute(flightPlanId, request.WaypointId, request.Sequence);
            }
            catch (DomainException exception)
            {
                return Results.BadRequest(new { error = exception.Message });
            }

            if (!await dbContext.waypoint.AnyAsync(x => x.WaypointId == route.WaypointId, cancellationToken))
            {
                return Results.BadRequest(new { error = "Waypoint was not found." });
            }

            if (await dbContext.flight_plan_route.AnyAsync(
                    x => x.FlightPlanId == flightPlanId && x.Sequence == route.Sequence,
                    cancellationToken))
            {
                return Results.Conflict(new { error = "A route entry already uses this sequence." });
            }

            await dbContext.flight_plan_route.AddAsync(route, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Created($"/flight-plans/{flightPlanId}/routes/{route.Sequence}", route);
        });
    }
}
