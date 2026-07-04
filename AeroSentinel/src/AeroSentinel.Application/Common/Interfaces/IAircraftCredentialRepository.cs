namespace AeroSentinel.Application.Common.Interfaces;

public interface IAircraftCredentialRepository
{
    Task<AircraftCredential?> GetAircraftCredentialAsync(string ICAO24, CancellationToken cancellationToken);

    Task SaveChangesAsync(AircraftCredential credential, CancellationToken cancellationToken);
}