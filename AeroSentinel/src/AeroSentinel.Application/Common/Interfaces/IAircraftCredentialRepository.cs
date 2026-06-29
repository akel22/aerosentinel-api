using AeroSentinel.Domain.Entities;

namespace AeroSentinel.Application.Common.Interfaces;

public interface IAircraftCredentialRepository
{
    Task<AircraftCredential?> GetAircraftCredentialAsync(Guid credentialId, CancellationToken cancellationToken);

    Task SaveChangesAsync(AircraftCredential credential, CancellationToken cancellationToken);
}