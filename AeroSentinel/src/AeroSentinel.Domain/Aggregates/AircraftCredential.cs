using System;

namespace AeroSentinel.Domain.Entities;

public sealed class AircraftCredential
{
    public string ICAO24 { get; init;} = null!; //FK to AircraftProfile.ICAO24

    private readonly byte[] _secretVerificationKey = null!; // Stored securely as a byte array, not exposed directly

    public ReadOnlySpan<byte> SecretVerificationKey =>
        _secretVerificationKey.AsSpan();

    private AircraftCredential()
    {
        // private constructor for ORM and serialization when Querying
    }
    public AircraftCredential(string aircraftId, byte[] secretVerificationKey)
    {

        ICAO24 = AircraftProfileValidation
            .RequireValidICAO24(aircraftId, nameof(aircraftId));
        

        _secretVerificationKey = AircraftCredentialValidation
            .RequireValidSecretVerificationKey(secretVerificationKey, ICAO24);
            
    }

    public bool MatchesAircraft(string ICAO24)
    {
        
        ICAO24 = AircraftProfileValidation.RequireValidICAO24(ICAO24, nameof(ICAO24));

        return ICAO24.Equals(
            this.ICAO24,
            StringComparison.OrdinalIgnoreCase);
    }
}