namespace AeroSentinel.Application.Common.Interfaces;
public interface IAircraftTelemetryRepository
{
    Task<FlightTelemetry?> GetByCompositeIndexAsync(long sequence, Guid flightPlanId, CancellationToken cancellationToken = default);
    Task<FlightTelemetry?> GetLatestFlightTelemetryAsync(string callsign, DateTime timestampUtc, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(FlightTelemetry telemetry, CancellationToken cancellationToken = default);
    Task<FlightTelemetry?> GetByMessageIdAsync(Guid guid, CancellationToken cancellationToken = default);
     Task<IEnumerable<FlightTelemetry?>> GetAllAsync(CancellationToken cancellationToken = default);

}
