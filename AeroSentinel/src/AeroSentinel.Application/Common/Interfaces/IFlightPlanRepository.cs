using AeroSentinel.Domain.Aggregates;

namespace AeroSentinel.Application.Common.Interfaces;

public interface IFlightPlanRepository
{
    Task<IReadOnlyList<FlightPlan>> GetLatestAsync(int count, CancellationToken cancellationToken = default);
}