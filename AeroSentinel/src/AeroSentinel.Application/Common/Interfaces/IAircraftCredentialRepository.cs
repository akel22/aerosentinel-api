using AeroSentinel.Domain.Entities;

namespace AeroSentinel.Application.Common.Interfaces;

public interface IAircraftCredentialRepository
{
    Task<AircraftCredential?> GetByIdAsync(Guid credentialId, CancellationToken cancellationToken);
}