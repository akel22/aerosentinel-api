using AeroSentinel.Domain.Extensions;

namespace AeroSentinel.Infrastructure.Repositories
{
    public sealed class AircraftCredentialRepository : IAircraftCredentialRepository
    {
        ApplicationDbContext _applicationDbContext;
        public AircraftCredentialRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IReadOnlyList<AircraftCredential>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.aircraft_credential
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        async public Task<AircraftCredential?> GetAircraftCredentialAsync(string ICAO24, CancellationToken cancellationToken)
        {
            var credential = await _applicationDbContext.aircraft_credential.FirstOrDefaultAsync(c => c.ICAO24 == ICAO24, cancellationToken: cancellationToken);

            if (credential is null)
            {
                throw new AircraftCredentialException(
                credential?.ICAO24, credential?.CredentialId, "Credential is null");
            }

            if (credential.Status == CredentialStatus.Inactive){

                throw new AircraftCredentialException(
                 credential?.ICAO24, credential?.CredentialId, $"Inactive Credential for {credential?.ICAO24}"
                );
            }

            return credential;
        }

        public async Task SaveChangesAsync(AircraftCredential credential, CancellationToken cancellationToken = default)
        {
            var existingCredential = await  _applicationDbContext.aircraft_credential.AsNoTracking().
            AnyAsync(c => c.ICAO24 == credential.ICAO24, cancellationToken);

            if (existingCredential)
            {
                throw new AircraftCredentialException(credential.ICAO24, 
                credential.CredentialId, "Credential for that ICAO24 already exists");

            }

            await _applicationDbContext.AddAsync(credential, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

        }

    }
}