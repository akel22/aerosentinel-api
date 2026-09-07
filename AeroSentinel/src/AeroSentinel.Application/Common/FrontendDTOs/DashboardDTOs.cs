using System;
using System.Collections.Generic;
using AeroSentinel.Domain.Extensions;

namespace AeroSentinel.Application.Common.DTOs;

public sealed record DashboardSnapshotDTO(
    DateTime Timestamp,
    DashboardSummaryDTO Summary,
    IReadOnlyList<TelemetryTrendPointDTO> TelemetryTrend,
    IReadOnlyList<LatestAircraftStatusDTO> AircraftStatus,
    IReadOnlyList<SecurityEventDTO> SecurityEvents,
    IReadOnlyList<FailedTelemetryDTO> FailedTelemetry,
    IReadOnlyList<LatestTelemetryDTO> LatestTelemetry,
    IReadOnlyList<FinishedFlightDTO> FinishedFlights);

public sealed record DashboardSummaryDTO(
    int AircraftProfilesCount,
    int TotalTelemetry,
    int VerifiedTelemetry,
    int FailedTelemetry,
    int CredentialsCount,
    double VerificationPercentage,
    int ActiveAircraft);

public sealed record TelemetryTrendPointDTO(
    string Hour,
    int Count);

public sealed record LatestAircraftStatusDTO(
    string Callsign,
    string Icao24,
    string Status,
    string LastTelemetry,
    string Authentication,
    double Altitude,
    double Speed);

public sealed record SecurityEventDTO(
    int Id,
    string Event,
    string Aircraft,
    DateTime Timestamp,
    string Severity);

public sealed record FailedTelemetryDTO(
    Guid MessageId,
    string Callsign,
    string Icao24,
    DateTime Timestamp,
    string Type,
    string Status,
    string Reason);

public sealed record LatestTelemetryDTO(
    Guid MessageId,
    string Callsign,
    string Icao24,
    string Status,
    DateTime Timestamp,
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
    string DelayMinutes);