using System;

namespace AeroSentinel.Domain.Entities;

public sealed class AircraftCredential
{
    public string AircraftId { get; private set; }

    private readonly byte[] _secretVerificationKey;

    public ReadOnlySpan<byte> SecretVerificationKey =>
        _secretVerificationKey.AsSpan();

    public AircraftCredential(string aircraftId, byte[] secretVerificationKey)
    {

        AircraftId = AircraftProfileValidation
            .RequireValidAircraftID(aircraftId, nameof(aircraftId));
        

        _secretVerificationKey = AircraftCredentialValidation
            .RequireValidSecretVerificationKey(secretVerificationKey, AircraftId);
            
    }

    public bool MatchesAircraft(string aircraftId)
    {
        
        aircraftId = AircraftProfileValidation.RequireValidAircraftID(aircraftId, nameof(aircraftId));

        return AircraftId.Equals(
            aircraftId,
            StringComparison.OrdinalIgnoreCase);
    }
}