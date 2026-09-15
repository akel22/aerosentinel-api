using AeroSentinel.Domain.Extensions;

namespace AeroSentinel.Infrastructure.Repositories
{
    public sealed class AircraftCredentialRepository : IAircraftCredentialRepository
    {
        ApplicationDbContext _applicationDbContext;
        ICredentialEncryptionService _encryptionService;
        public AircraftCredentialRepository(ApplicationDbContext applicationDbContext, 
                                            ICredentialEncryptionService encryptionService)
        {
            _applicationDbContext = applicationDbContext;
            _encryptionService = encryptionService;
        }

        public async Task<IReadOnlyList<AircraftCredential>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.aircraft_credential
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<CredentialDTO?> GetAircraftCredentialAsync(string ICAO24, CancellationToken cancellationToken)
        {
            var credential = await _applicationDbContext.aircraft_credential.FirstOrDefaultAsync(c => c.ICAO24 == ICAO24, cancellationToken: cancellationToken);

            if (credential is null)
            {
                throw new AircraftCredentialException(
                credential?.ICAO24, credential?.CredentialId, $"Credential for {ICAO24} is null");
            }

            if (credential.Status == CredentialStatus.Inactive){

                throw new AircraftCredentialException(
                 credential?.ICAO24, credential?.CredentialId, $"Inactive Credential for {credential?.ICAO24}"
                );
            }   

            var decryptedKey = _encryptionService.Decrypt(credential.VerificationKey);

            return new CredentialDTO(
                CredentialId: credential.CredentialId,
                ICAO24: credential.ICAO24,
                VerificationKey: decryptedKey,
                Status: credential.Status,
                CreatedUtc: credential.CreatedUtc  
                ); 

             // adjust to match your actual entity/DTO shape
        }

        public async Task SaveChangesAsync(CredentialDTO credentialDTO, CancellationToken cancellationToken = default)
        {
            var existingCredential = await  _applicationDbContext.aircraft_credential.AsNoTracking().
            AnyAsync(c => c.ICAO24 == credentialDTO.ICAO24, cancellationToken);

            if (existingCredential)
            {
                throw new AircraftCredentialException(credentialDTO.ICAO24, 
                credentialDTO.CredentialId, "Credential for that ICAO24 already exists");

            }

            var encryptedKey = _encryptionService.Encrypt(credentialDTO.VerificationKey);

            var encryptedCredential =  new AircraftCredential(credentialDTO.ICAO24, encryptedKey);
                                                                   
            await _applicationDbContext.AddAsync(encryptedCredential, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

        }
    }
}