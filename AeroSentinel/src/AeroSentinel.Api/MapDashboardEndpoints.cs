using System.Globalization;
using AeroSentinel.Domain.Extensions;

namespace AeroSentinel.Api;

public static class MapDashboardEndpoints
{
    public static void MapHttpDashboardEndpoints(this WebApplication app)
    {
        app.MapGet("/dashboard", async (
            IAircraftProfileRepository profileRepository,
            IAircraftTelemetryRepository telemetryRepository,
            IAircraftCredentialRepository credentialRepository,
            CancellationToken cancellationToken) =>
        {
            var profiles = await profileRepository.GetAllAsync(cancellationToken);
            var telemetry = (await telemetryRepository.GetAllAsync(cancellationToken))
                .Where(frame => frame is not null)
                .Select(frame => frame!)
                .OrderByDescending(frame => frame.TimestampUtc)
                .ToList();
            var credentials = await credentialRepository.GetAllAsync(cancellationToken);
            var verified = telemetry.Count(frame => frame.Status == TelemetryStatus.Verified);
            var failed = telemetry.Count - verified;
            var latestByAircraft = telemetry
                .GroupBy(frame => frame.ICAO24, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);

            var failedTelemetry = telemetry
                .Where(frame => frame.Status != TelemetryStatus.Verified)
                .Select(frame => new FailedTelemetryDTO(
                    frame.MessageId.ToString(),
                    frame.Callsign,
                    frame.ICAO24,
                    FormatTimestamp(frame.TimestampUtc),
                    "Authentication",
                    frame.Status.ToString(),
                    frame.FailureReason ?? "Telemetry verification failed"))
                .ToList();

            var snapshot = new DashboardSnapshotDTO(
                DateTime.UtcNow,
                new DashboardSummaryDTO(
                    profiles.Count,
                    telemetry.Count,
                    verified,
                    failed,
                    credentials.Count,
                    telemetry.Count == 0 ? 0 : verified * 100d / telemetry.Count,
                    latestByAircraft.Values.Count(frame => frame.Status.ToString() == "Verified")),
                telemetry
                    .GroupBy(frame => new { frame.TimestampUtc.Date, frame.TimestampUtc.Hour })
                    .OrderBy(group => group.Key.Date)
                    .ThenBy(group => group.Key.Hour)
                    .Select(group => new TelemetryTrendPointDTO($"{group.Key.Hour:00}:00", group.Count()))
                    .ToList(),
                profiles.Select(profile =>
                {
                    latestByAircraft.TryGetValue(profile.ICAO24, out var latest);
                    return new AircraftStatusDTO(
                        latest?.Callsign ?? "N/A",
                        profile.ICAO24,
                        latest?.Status.ToString() ?? "Unknown",
                        latest is null ? "No recent telemetry" : FormatTimestamp(latest.TimestampUtc),
                        latest?.Status.ToString() ?? "Unknown",
                        latest is null ? "n/a" : $"{latest.SpatialState.BaroAltitudeFeet:N0} ft",
                        latest is null ? "n/a" : $"{latest.SpatialState.GroundSpeedKnots:N0} kt");
                }).ToList(),
                failedTelemetry.Count == 0
                    ? [new SecurityEventDTO(1, "No failed telemetry records detected", "System", "Current snapshot", "INFO")]
                    : failedTelemetry.Select((frame, index) => new SecurityEventDTO(
                        index + 1,
                        "Telemetry verification failure",
                        frame.Aircraft,
                        frame.Timestamp,
                        "CRITICAL")).ToList(),
                failedTelemetry,
                telemetry.Take(10).Select(frame => new LatestTelemetryDTO(
                    frame.MessageId,
                    frame.Callsign,
                    frame.ICAO24,
                    frame.Status.ToString(),
                    frame.TimestampUtc,
                    frame.SequenceNumber,
                    frame.FailureReason ?? string.Empty)).ToList());

            return Results.Ok(snapshot);
        });
    }

    private static string FormatTimestamp(DateTime timestamp) =>
        timestamp.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
}