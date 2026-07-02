namespace AeroSentinel.Domain.Entities;

public sealed class AircraftCredential
{
    public Guid CredentialId {get; private set;}
    public string ICAO24 { get; init; } = null!;
    public AircraftProfile AircraftProfile { get; init; } = null!;
    public byte[] VerificationKey {get; init;} = null!;

    // public ReadOnlySpan<byte> VerificationKey =>
    //     _secretVerificationKey.AsSpan();
    public CredentialStatus Status { get; private set; }
    public DateTime CreatedUtc { get; private set; }
    private AircraftCredential()
    {
        // ORM / serialization
    }

    public AircraftCredential(Guid credentialId,
        string icao24,
        byte[] verificationKey)
    {

        CredentialId = credentialId;

        ICAO24 =
            AircraftProfileValidation
            .RequireValidICAO24(
                icao24,
                nameof(icao24));

        VerificationKey =
            AircraftCredentialValidation
            .RequireValidSecretVerificationKey(
                verificationKey,
                ICAO24);

        Status =
            CredentialStatus.Active;

        CreatedUtc =
            DateTime.UtcNow;
    }
}