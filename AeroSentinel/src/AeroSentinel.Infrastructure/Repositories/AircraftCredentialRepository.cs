public sealed class AircraftCredentialRepository : IAircraftCredentialRepository
{
    ApplicationDbContext _applicationDbContext;
    public AircraftCredentialRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    async public Task<AircraftCredential?> GetAircraftCredentialAsync(Guid credentialId, CancellationToken cancellationToken)
    {   
        var credential = await _applicationDbContext.AircraftCredentials.FindAsync(credentialId);

        if(credential is null) 
        {
            throw new AircraftCredentialException(
            credential?.ICAO24, credential?.CredentialId, "Credential is null");
        }

        return credential;
    }

    public async Task SaveChangesAsync(AircraftCredential credential, CancellationToken cancellationToken = default)
    {
        await _applicationDbContext.AddAsync(credential);
        
        await _applicationDbContext.SaveChangesAsync();

    }

}
