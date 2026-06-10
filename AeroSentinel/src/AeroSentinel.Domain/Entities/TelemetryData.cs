using System;
using AeroSentinel.Domain.Enums;
using AeroSentinel.Domain.Exceptions;

namespace AeroSentinel.Domain.Entities;

public sealed class FlightTelemetry
{
    // Identification Markers
    public string MessageId { get; }
    public string Icao24 { get; }
    public string Callsign { get; }
    public DateTime TimestampUtc { get; }

    // 3D Spatial Metrics
    public double Latitude { get; }
    public double Longitude { get; }
    public double AltitudeFeet { get; }

    // Kinematic Movement Vectors
    public double GroundSpeedKnots { get; }
    public double TrackDegrees { get; }
    public double VerticalRateFeetPerMinute { get; }

    // Cryptographic Security Envelope
    public long SequenceNumber { get; }
    public string Signature { get; }

    // System Evaluation Flags (Mutable internally by our validation engine)
    public TelemetryStatus Status { get; private set; }
    public string FailureReason { get; private set; }

    public FlightTelemetry(
        string messageId,
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
        string signature,
        string departureAirport,
        string destinationAirport,
        string currentWaypoint,
        string nextWaypoint)
    {
        // Enforce structural identity checks using our profile validation engine
        Icao24 = AircraftProfileValidation.RequireValidAircraftID(icao24, nameof(icao24));
        
        // Utilize dedicated telemetry validation rules for spatial boundaries
        MessageId = FlightTelemetryValidation.RequireValidMessageId(messageId, nameof(messageId));
        Latitude = FlightTelemetryValidation.RequireValidLatitude(latitude, nameof(latitude));
        Longitude = FlightTelemetryValidation.RequireValidLongitude(longitude, nameof(longitude));
        TrackDegrees = FlightTelemetryValidation.RequireValidTrackDegrees(trackDegrees, nameof(trackDegrees));

        if (string.IsNullOrWhiteSpace(signature))
            throw new ArgumentException("Cryptographic signature payload is mandatory.", nameof(signature));

        Callsign = callsign?.Trim().ToUpperInvariant() ?? "ANONYMOUS";
        TimestampUtc = timestampUtc;
        AltitudeFeet = altitudeFeet;
        GroundSpeedKnots = groundSpeedKnots;
        VerticalRateFeetPerMinute = verticalRateFeetPerMinute;
        SequenceNumber = sequenceNumber;
        Signature = signature.Trim();

     
        // Default state state upon record creation
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