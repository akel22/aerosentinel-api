using System.Globalization;

namespace AeroSentinel.Api;

public static class MapDashboardEndpoints
{
    public static void MapHttpDashboardEndpoints(this WebApplication app)
    {
        app.MapGet("/dashboard", async (
            IAircraftProfileRepository profileRepository,
            IAircraftTelemetryRepository telemetryRepository,
            IAircraftCredentialRepository credentialRepository,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var profiles = await profileRepository.GetAllAsync(cancellationToken);

            var telemetry = (await telemetryRepository.GetAllAsync(cancellationToken))
                .Where(frame => frame is not null)
                .Select(frame => frame!)
                .OrderByDescending(frame => frame.TimestampUtc)
                .ToList();

            var credentials = await credentialRepository.GetAllAsync(cancellationToken);
            
            var flightPlans = await dbContext.flight_plan
                .AsNoTracking()
                .OrderByDescending(plan => plan.EstimatedArrivalTimeUtc)
                .Take(10)
                .ToListAsync(cancellationToken);

            var snapshot = new DashboardSnapshotDTO(
                DateTime.UtcNow,
                new DashboardSummaryDTO(
                    profiles.Count,
                    telemetry.Count,
                    0,
                    0,
                    credentials.Count,
                    0,
                    profiles.Count),
                [.. telemetry
                    .Take(12)
                    .Select(frame => new TelemetryTrendPointDTO(
                        FormatTimestamp(frame.TimestampUtc),
                        1))],
                [.. telemetry
                    .Take(10)
                    .Select(tel => new AircraftStatusDTO(
                        tel.Callsign,
                        tel.ICAO24,
                        tel.Status.ToString(),
                        tel.TimestampUtc.ToString(),
                        tel.Squawk,
                        tel.SpatialState.GeoAltitudeFeet,
                        tel.SpatialState.GroundSpeedKnots))],
                telemetry
                    .Take(10)
                    .Select((frame, index) => new SecurityEventDTO(
                        index + 1,
                        frame.Status.ToString(),
                        frame.Callsign,
                        FormatTimestamp(frame.TimestampUtc),
                        "CRITICAL"))
                    .ToList(),
                telemetry
                    .Take(10)
                    .Select(frame => new FailedTelemetryDTO(
                        frame.MessageId.ToString(),
                        frame.Callsign,
                        frame.ICAO24,
                        FormatTimestamp(frame.TimestampUtc),
                        "Authentication",
                        frame.Status.ToString(),
                        frame.FailureReason ?? string.Empty))
                    .ToList(),
                telemetry
                    .Take(10)
                    .Select(frame => new LatestTelemetryDTO(
                        frame.MessageId,
                        frame.Callsign,
                        frame.ICAO24,
                        frame.Status.ToString(),
                        frame.TimestampUtc,
                        frame.SequenceNumber,
                        frame.FailureReason ?? string.Empty))
                    .ToList(),
                flightPlans
                    .Select(plan => new FinishedFlightDTO(
                        plan.FlightPlanId,
                        plan.Callsign,
                        plan.ICAO24,
                        plan.DepartureAirport,
                        plan.DestinationAirport,
                        plan.EstimatedArrivalTimeUtc,
                        plan.EstimatedArrivalTimeUtc,
                        0))
                    .ToList());

            return Results.Ok(snapshot);
        });
    }

    private static string FormatTimestamp(DateTime timestamp) =>
        timestamp.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
}
