namespace AeroSentinel.Domain.Entities.Validations;

public static class AircraftCredentialValidation
{
    public static byte[] RequireValidSecretVerificationKey(byte[] secretVerificationKey,
        string aircraftId)
    {
        if (secretVerificationKey is null)
            throw new InvalidVerificationKeyException(
                aircraftId,
                "Verification key cannot be null.");

        if (secretVerificationKey.Length < 32)
            throw new InvalidVerificationKeyException(
                aircraftId,
                "Verification key must be at least 32 bytes (256 bits).");

        return (byte[])secretVerificationKey.Clone();
        // a clone is returned to prevent external modification of the internal key storage

    }
}
