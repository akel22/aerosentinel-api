using AeroSentinel.Domain.Aggregates;

namespace AeroSentinel.Application.Common.Interfaces;

public interface IFlightPlanRepository
{
    Task<IReadOnlyList<FlightPlan>> GetFininishedFlightsAsync(CancellationToken cancellationToken = default);

    
}