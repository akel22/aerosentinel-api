using AeroSentinel.Domain.Aggregates;

namespace AeroSentinel.Application.Common.Interfaces
{
    public interface IWaypointRepository
    {
        public Task<IReadOnlyList<Waypoint>> GetAllWaypointsAsync(CancellationToken cancellationToken = default);

    }
}