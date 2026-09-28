namespace AeroSentinel.Infrastructure.Repositories;
public sealed class AircraftProfileRepository : IAircraftProfileRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AircraftProfileRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;

    }

    public async Task<IReadOnlyList<AircraftProfile>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _applicationDbContext.aircraft_profile
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<AircraftProfile?> GetByICAO24Async(string icao24, CancellationToken cancellationToken = default)
    {
        var profile = await _applicationDbContext.aircraft_profile
            .AsNoTracking()
            .FirstOrDefaultAsync(profile => profile.ICAO24 == icao24, cancellationToken) ?? throw new InvalidAircraftIdentifierException(icao24, "A profile with this ICAO24 does not exist");

        return profile;
    }

    public async Task SaveChangesAsync(AircraftProfile profile, CancellationToken cancellationToken = default)
    {
       await _applicationDbContext.AddAsync(profile, cancellationToken);

       await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftProfile profile, CancellationToken cancellationToken = default)
    {
        var profileExists = await _applicationDbContext.aircraft_profile
            .AnyAsync(existingProfile => existingProfile.ICAO24 == profile.ICAO24, cancellationToken);

        if (!profileExists)
        {
            throw new InvalidAircraftIdentifierException(profile.ICAO24, "A profile with this ICAO24 does not exist");
        }

        _applicationDbContext.aircraft_profile.Update(profile);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
    
}
