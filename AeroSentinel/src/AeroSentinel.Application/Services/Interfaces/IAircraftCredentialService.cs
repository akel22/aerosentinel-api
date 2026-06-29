namespace AeroSentinel.Application.Common.Interfaces;

public interface IAircraftCredentialCacheService
{
    Task<byte[]?> GetSharedVerificationAsync(Guid credentialId, CancellationToken cancellationToken);
}