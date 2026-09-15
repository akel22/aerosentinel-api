using AeroSentinel.Domain.Aggregates;

namespace AeroSentinel.Application.Common.Interfaces;

public interface IAircraftCredentialRepository
{
    Task<IReadOnlyList<AircraftCredential>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CredentialDTO?> GetAircraftCredentialAsync(string ICAO24, CancellationToken cancellationToken);
    Task SaveChangesAsync(CredentialDTO credentialDTO, CancellationToken cancellationToken);
}