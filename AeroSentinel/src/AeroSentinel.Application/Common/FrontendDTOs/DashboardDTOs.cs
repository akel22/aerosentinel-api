using AeroSentinel.Domain.Extensions;

namespace AeroSentinel.Application.Common.DTOs;

public sealed record DashboardSnapshotDTO(
    DashboardSummaryDTO Summary,
    IReadOnlyList<AircraftStatusDTO> Aircraft,
    IReadOnlyList<LatestTelemetryDTO> LatestTelemetry,
    IReadOnlyList<FinishedFlightDTO> FinishedFlights);

public sealed record DashboardSummaryDTO(
    int RegisteredAircraft,
    int TelemetryFrames,
    int VerifiedFrames,
    int FailedFrames,
    int CredentialedAircraft,
    double AuthenticationRate,
    int ActiveAircraft);

public sealed record AircraftStatusDTO(
    string Callsign,
    string Icao24,
    string Status,
    string LastTelemetry,
    string Authentication,
    double Altitude,
    double Speed);

public sealed record LatestTelemetryDTO(
    Guid MessageId,
    string Callsign,
    string Icao24,
    string Status,
    DateTime TimestampUtc,
    long SequenceNumber,
    string FailureReason);

public sealed record FinishedFlightDTO(
    Guid FlightPlanId,
    string Callsign,
    string Icao24,
    string DepartureAirport,
    string DestinationAirport,
    DateTime EstimatedArrivalTimeUtc,
    DateTime ActualArrivalTimeUtc,
    TimeOnly ArrivalVarianceMinutes);