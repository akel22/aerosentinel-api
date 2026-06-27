namespace AeroSentinel.Application.Common.Interfaces;
public interface IAircraftTelemetryRepository
{
    Task SaveAsync(FlightTelemetry telemetry, CancellationToken cancellationToken = default);

     Task<FlightTelemetry?> GetByCompositeKeyAsync(long sequence, CancellationToken cancellationToken = default);
    //GET BY 3 COMPOSITE KEYS
 
}
