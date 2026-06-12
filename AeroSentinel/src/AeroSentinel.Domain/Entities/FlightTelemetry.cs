using System;
using AeroSentinel.Domain.Enums;
using AeroSentinel.Domain.Exceptions;

namespace AeroSentinel.Domain.Entities;

public sealed class FlightTelemetry
{
    // Identification Markers
    public Guid MessageId { get; }
    public string ICAO24 { get; } = null!;
    public string Callsign { get; } = null!;
    public DateTime TimestampUtc { get; } 

    // 3D Spatial Metrics (Strict Philippine FIR Boundaries)
    public double Latitude { get; }
    public double Longitude { get; }
    public double AltitudeFeet { get; }

    // Kinematic Movement Vectors (Commercial Fleet Thresholds)
    public double GroundSpeedKnots { get; }
    public double TrackDegrees { get; }
    public double VerticalRateFeetPerMinute { get; }

    // Cryptographic Security Envelope
    public long SequenceNumber { get; }
    public string Signature { get; } = null!;

    // System Evaluation Flags (Mutable internally by our validation pipelines)
    public TelemetryStatus Status { get; private set; }
    public string FailureReason { get; private set; } = null!;

    private FlightTelemetry()
    {
        // Parameterless constructor for ORM and serialization
    }
    public FlightTelemetry(
        string icao24,
        string callsign,
        DateTime timestampUtc,
        double latitude,
        double longitude,
        double altitudeFeet,
        double groundSpeedKnots,
        double trackDegrees,
        double verticalRateFeetPerMinute,
        long sequenceNumber,
        string signature)
    {
        // 1. Generate Immutable Runtime Tracking Key
        MessageId = Guid.NewGuid();

        // 2. Execute Structural Identity & Basic Flight Context Validation
        ICAO24 = AircraftProfileValidation.RequireValidAircraftID(icao24, nameof(icao24));
        Callsign = PhilippineFlightPlanDetailsValidation.RequireValidCallsign(callsign, nameof(callsign));

        // 3. Execute Space-Time Geofence Enforcements (Philippine FIR Checkpoints)
        Latitude = FlightTelemetryValidation.RequireValidLatitude(latitude, nameof(latitude));
        Longitude = FlightTelemetryValidation.RequireValidLongitude(longitude, nameof(longitude));
        TimestampUtc = FlightTelemetryValidation.RequireValidTimestamp(timestampUtc, nameof(timestampUtc));

        // 4. Execute Commercial Fleet Performance & Vector Assertions
        AltitudeFeet = FlightTelemetryValidation.RequireValidAltitudeFeet(altitudeFeet, nameof(altitudeFeet));
        GroundSpeedKnots = FlightTelemetryValidation.RequireValidGroundSpeedKnots(groundSpeedKnots, nameof(groundSpeedKnots));
        TrackDegrees = FlightTelemetryValidation.RequireValidTrackDegrees(trackDegrees, nameof(trackDegrees));
        VerticalRateFeetPerMinute = FlightTelemetryValidation.RequireValidVerticalRateFeetPerMinute(verticalRateFeetPerMinute, nameof(verticalRateFeetPerMinute));

        // 5. Execute Security Envelope Initializations
        SequenceNumber = FlightTelemetryValidation.RequireValidSequenceNumber(sequenceNumber, nameof(sequenceNumber));
        Signature = FlightTelemetryValidation.RequireValidSignature(signature, nameof(signature));

        // 6. Establish Ingestion Pipeline State Machine Default Values
        Status = TelemetryStatus.Stale;
        FailureReason = "Pending multi-stage verification analysis pipeline.";
    }

    public void MarkAsCompromised(string explanation)
    {
        Status = TelemetryStatus.Compromised;
        FailureReason = explanation;
    }

    public void MarkAsSpoofed(string explanation)
    {
        Status = TelemetryStatus.Spoofed;
        FailureReason = explanation;
    }

    public void MarkAsVerified()
    {
        Status = TelemetryStatus.Verified;
        FailureReason = string.Empty;
    }
}