namespace AeroSentinel.Infrastructure.Repositories;

public sealed class FlightPlanRepository : IFlightPlanRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public FlightPlanRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IReadOnlyList<FlightPlan>> GetLatestAsync(
        int count,
        CancellationToken cancellationToken = default)
    {
        return await _applicationDbContext.flight_plan
            .AsNoTracking()
            .OrderByDescending(plan => plan.EstimatedArrivalTimeUtc)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}