namespace AeroSentinel.Application.Common.DTOs;

public readonly record struct RawPayloadSignDTO(
    long Sequence,
    string ICAO24,
    Guid CredentialId,
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
    double RollAngleDegrees
);

[JsonSerializable(typeof(RawPayloadSignDTO))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified,
    WriteIndented = false
)]
public partial class TelemetryJsonContext : JsonSerializerContext
{
}