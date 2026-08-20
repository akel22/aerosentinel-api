namespace AeroSentinel.Application.Services.Interfaces;

public interface IAircraftCredentialCacheService
{
    Task<byte[]?> GetSharedVerificationAsync(string ICAO24, CancellationToken cancellationToken);
}