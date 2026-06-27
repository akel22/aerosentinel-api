namespace AeroSentinel.Application.Common.Interfaces;

public interface IAircraftCredentialCacheService
{
    Task<AircraftCredential?> GetCredentialAsync( Guid credentialId, CancellationToken cancellationToken);
}