namespace AeroSentinel.Application.Common.DTOs;

public sealed record DashboardSnapshotDTO(
    DateTime GeneratedAt,
    DashboardSummaryDTO Summary,
    IReadOnlyList<TelemetryTrendPointDTO> TelemetryTrend,
    IReadOnlyList<AircraftStatusDTO> Aircraft,
    IReadOnlyList<SecurityEventDTO> SecurityEvents,
    IReadOnlyList<FailedTelemetryDTO> FailedTelemetryLog,
    IReadOnlyList<LatestTelemetryDTO> LatestTelemetry);

public sealed record DashboardSummaryDTO(
    int RegisteredAircraft,
    int TelemetryFrames,
    int VerifiedFrames,
    int FailedFrames,
    int CredentialedAircraft,
    double AuthenticationRate,
    int ActiveAircraft);

public sealed record TelemetryTrendPointDTO(string Time, int Volume);

public sealed record AircraftStatusDTO(
    string Callsign,
    string Icao24,
    string Status,
    string LastTelemetry,
    string Authentication,
    string Altitude,
    string Speed);

public sealed record SecurityEventDTO(
    int Id,
    string Type,
    string Aircraft,
    string Timestamp,
    string Severity);

public sealed record FailedTelemetryDTO(
    string Id,
    string Callsign,
    string Aircraft,
    string Timestamp,
    string Category,
    string Status,
    string FailureReason);

public sealed record LatestTelemetryDTO(
    Guid MessageId,
    string Callsign,
    string Icao24,
    string Status,
    DateTime TimestampUtc,
    long SequenceNumber,
    string FailureReason);