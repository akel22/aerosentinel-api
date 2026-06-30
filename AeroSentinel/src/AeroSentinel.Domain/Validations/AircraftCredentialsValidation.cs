namespace AeroSentinel.Domain.Entities.Validations;

public static class AircraftCredentialValidation
{
    public static byte[] RequireValidSecretVerificationKey(byte[] secretVerificationKey,
        string? ICAO24)
    {
        if (secretVerificationKey is null)
            throw new InvalidVerificationKeyException(
                secretVerificationKey,
                $"Verification key for aircraft [{ICAO24}] cannot be null.");

        if (secretVerificationKey.Length < 32)
            throw new InvalidVerificationKeyException(
                secretVerificationKey,
                $"Verification key for aircraft [{ICAO24}] must be at least 32 bytes (256 bits).");

        return [.. secretVerificationKey];
        // a clone is returned to prevent external modification of the internal key storage

    }
}
