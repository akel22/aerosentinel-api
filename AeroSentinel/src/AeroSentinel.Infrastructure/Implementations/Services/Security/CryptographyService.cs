namespace AeroSentinel.Infrastructure.Implementations.Services.Security;

public sealed class CryptographyService : ICryptographyService
{
    public bool VerifyPayloadSignature(RawPayloadDTO payload, byte[] secretKey)
    {

        if (string.IsNullOrWhiteSpace(payload.Signature)) return false;

        var hashTarget = new RawPayloadSignDTO
    (
        payload.Sequence,
        payload.ICAO24,
        payload.Callsign,
        payload.Squawk,
        payload.TimestampUTC,
        payload.Latitude,
        payload.Longitude,
        payload.BaroAltitudeFeet,
        payload.GeoAltitudeFeet,
        payload.GroundSpeedKnots,
        payload.TrackAngleDegrees,
        payload.VerticalRateFpm,
        payload.SelectedAltitudeFeet,
        payload.IndicatedAirspeedKnots,
        payload.MagneticHeadingDegrees,
        payload.RollAngleDegrees
    );

        // 1. Serialize the object into a deterministic binary JSON footprint
        byte[] messageBytes = JsonSerializer.SerializeToUtf8Bytes(hashTarget,
        TelemetryJsonContext.Default.RawPayloadSignDTO);

        // 2. Compute the HMAC-SHA256 digest using the shared secret key
        using var hmac = new HMACSHA256(secretKey);
        byte[] computedHashBytes = hmac.ComputeHash(messageBytes);
        
        string locallyComputedSignature = Convert.ToHexString(computedHashBytes);
        string incomingSignature = payload.Signature.ToUpperInvariant().Trim();

        // 3. Constant-Time Byte Comparison to prevent timing side-channel attacks
        byte[] incomingSignatureBytes = Encoding.UTF8.GetBytes(incomingSignature);
        byte[] computedSignatureBytes = Encoding.UTF8.GetBytes(locallyComputedSignature);

        return CryptographicOperations.FixedTimeEquals(incomingSignatureBytes, computedSignatureBytes);
        //scans the whole bytes before stopping, not stopping the nanosecond it is wrong
    }
         
}