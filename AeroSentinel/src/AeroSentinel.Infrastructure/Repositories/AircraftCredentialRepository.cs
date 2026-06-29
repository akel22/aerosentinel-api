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

        return credential;
    }

}
