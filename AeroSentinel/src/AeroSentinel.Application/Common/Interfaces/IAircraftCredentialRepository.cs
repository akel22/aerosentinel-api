using AeroSentinel.Domain.Aggregates;

namespace AeroSentinel.Application.Common.Interfaces;

public interface IAircraftCredentialRepository
{
    Task<IReadOnlyList<AircraftCredential>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AircraftCredential?> GetAircraftCredentialAsync(string ICAO24, CancellationToken cancellationToken);

    Task SaveChangesAsync(AircraftCredential credential, CancellationToken cancellationToken);
}