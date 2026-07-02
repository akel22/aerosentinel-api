using System.Text.Json.Serialization;

namespace AeroSentinel.Application.Common.DTOs;

public record RawPayloadDTO(
    Guid FlightPlanId,
    long Sequence,
    string ICAO24,
    string Callsign,
    string Squawk,
    DateTime TimestampUTC,
    double Latitude,
    double Longitude,
    double BaroAltitudeFeet,
    double GeoAltitudeFeet,
    double GroundSpeedKnots,
    double TrackAngleDegrees,
    double VerticalRateFpm,
    double SelectedAltitudeFeet,
    double IndicatedAirspeedKnots,
    double MagneticHeadingDegrees,
    double RollAngleDegrees,

    // The signature itself is excluded from the hashing process internally
    string Signature
);