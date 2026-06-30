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
        var profile = await _applicationDbContext.aircraft_profile.FindAsync(icao24);

        if(profile is null)
        {
            throw new InvalidAircraftIdentifierException(profile?.ICAO24, "A profile with this ICAO24 is not existing");
        }

        return profile;
    }

    public async Task SaveChangesAsync(AircraftProfile profile, CancellationToken cancellationToken = default)
    {
       await _applicationDbContext.AddAsync(profile);

       await _applicationDbContext.SaveChangesAsync();
    }
    
}