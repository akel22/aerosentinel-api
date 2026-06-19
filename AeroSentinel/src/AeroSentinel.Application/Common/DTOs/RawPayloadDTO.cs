namespace AeroSentinel.Application.Common.DTOs;

public record RawPayloadDTO(
    long Sequence,
    string? ICAO24,
    string? Callsign,
    string? Squawk,
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
    double  RollAngleDegrees,
    string Signature
);