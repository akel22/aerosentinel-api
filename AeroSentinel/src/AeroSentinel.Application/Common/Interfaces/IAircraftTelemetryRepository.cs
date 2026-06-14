namespace AeroSentinel.Application.Common.Interfaces;
public interface IAircraftTelemetryRepository
{
    Task SaveAsync(FlightTelemetry telemetry, CancellationToken cancellationToken = default);
 
}
