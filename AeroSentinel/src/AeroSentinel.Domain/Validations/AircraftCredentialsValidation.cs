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

        if (secretVerificationKey.Length != 32)
            throw new InvalidVerificationKeyException(
                secretVerificationKey,
                $"Verification key for aircraft [{ICAO24}] must be at exact 32 bytes (256 bits).");

        return [.. secretVerificationKey];
        // a clone is returned to prevent external modification of the internal key storage

    }

    public static Guid RequireValidCredentialId(Guid guid, string? propertyName){

        if(guid == Guid.Empty)
        {
            throw new GuidException(guid, $"{propertyName} must not be empty");
        }

        return guid;
    }

    
}
