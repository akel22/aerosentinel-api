namespace AeroSentinel.Infrastructure.Repositories;
public sealed class AircraftProfileRepository : IAircraftProfileRepository
{
    ApplicationDbContext _applicationDbContext;
    public AircraftProfileRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;

    }
    public async Task<AircraftProfile?> GetByICAO24Async(string icao24, CancellationToken cancellationToken = default)
    {
        var profile = await _applicationDbContext.AircraftProfiles.FindAsync(icao24);

        

        
    }
    
}