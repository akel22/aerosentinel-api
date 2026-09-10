using AeroSentinel.Domain.Extensions;

namespace AeroSentinel.Infrastructure.Repositories;

public sealed class FlightPlanRepository : IFlightPlanRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public FlightPlanRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IReadOnlyList<FlightPlan>> GetFininishedFlightsAsync(CancellationToken cancellationToken = default)
    {
        return await _applicationDbContext.flight_plan.Where(x => x.FlightStatus == FlightStatus.Finished)
        .ToListAsync(cancellationToken: cancellationToken);
    }
}