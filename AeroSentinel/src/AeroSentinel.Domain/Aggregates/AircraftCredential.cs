namespace AeroSentinel.Domain.Entities;

public sealed class AircraftCredential
{
    public string ICAO24 { get; init; } = null!;

    private readonly byte[] _secretVerificationKey = null!;

    public ReadOnlySpan<byte> VerificationKey =>
        _secretVerificationKey.AsSpan();

    public int KeyVersion { get; private set; }

    public CredentialStatus Status { get; private set; }

    public DateTime CreatedUtc { get; private set; }

    private AircraftCredential()
    {
        // ORM / serialization
    }

    public AircraftCredential(
        string aircraftId,
        byte[] secretVerificationKey)
    {
        ICAO24 =
            AircraftProfileValidation
            .RequireValidICAO24(
                aircraftId,
                nameof(aircraftId));

        _secretVerificationKey =
            AircraftCredentialValidation
            .RequireValidSecretVerificationKey(
                secretVerificationKey,
                ICAO24);

        KeyVersion = 1;

        Status =
            CredentialStatus.Active;

        CreatedUtc =
            DateTime.UtcNow;
    }
}