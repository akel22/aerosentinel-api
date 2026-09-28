using AeroSentinel.Domain.Extensions;

namespace AeroSentinel.Infrastructure.Repositories;

public sealed class WaypointRepository : IWaypointRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public WaypointRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IReadOnlyList<Waypoint>> GetAllWaypointsAsync(CancellationToken cancellationToken = default)
    {
       return await _applicationDbContext.waypoint.ToListAsync(cancellationToken);

    }

    public async Task<Waypoint?> GetByWaypointIdAsync(string waypointId, CancellationToken cancellationToken = default)
    {
    return await _applicationDbContext.waypoint.FindAsync([waypointId], cancellationToken);

    }
}