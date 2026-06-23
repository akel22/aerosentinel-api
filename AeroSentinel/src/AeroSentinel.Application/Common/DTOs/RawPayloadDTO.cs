using System.Text.Json.Serialization;

namespace AeroSentinel.Application.Common.DTOs;

public record RawPayloadDTO(
    [property: JsonPropertyOrder(1)] long Sequence,
    [property: JsonPropertyOrder(2)] string ICAO24,
    [property: JsonPropertyOrder(3)] Guid CredentialId,
    [property: JsonPropertyOrder(4)] string Callsign,
    [property: JsonPropertyOrder(5)] string Squawk,
    [property: JsonPropertyOrder(6)] DateTime TimestampUTC,
    [property: JsonPropertyOrder(7)] double Latitude,
    [property: JsonPropertyOrder(8)] double Longitude,
    [property: JsonPropertyOrder(9)] double BaroAltitudeFeet,
    [property: JsonPropertyOrder(10)] double GeoAltitudeFeet,
    [property: JsonPropertyOrder(11)] double GroundSpeedKnots,
    [property: JsonPropertyOrder(12)] double TrackAngleDegrees,
    [property: JsonPropertyOrder(13)] double VerticalRateFpm,
    [property: JsonPropertyOrder(14)] double SelectedAltitudeFeet,
    [property: JsonPropertyOrder(15)] double IndicatedAirspeedKnots,
    [property: JsonPropertyOrder(16)] double MagneticHeadingDegrees,
    [property: JsonPropertyOrder(17)] double RollAngleDegrees,

    // The signature itself is excluded from the hashing process internally
    string Signature
);