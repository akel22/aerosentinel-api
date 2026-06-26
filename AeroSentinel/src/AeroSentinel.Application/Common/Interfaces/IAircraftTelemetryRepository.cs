namespace AeroSentinel.Application.Common.Interfaces;
public interface IAircraftTelemetryRepository
{
    Task SaveAsync(FlightTelemetry telemetry, CancellationToken cancellationToken = default);

     Task<FlightTelemetry?> GetBySequenceAsync(long sequence, CancellationToken cancellationToken = default);

 
}
