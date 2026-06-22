namespace AeroSentinel.Application.Common.Interfaces;
public interface IAircraftTelemetryRepository
{
    Task SaveAsync(FlightTelemetry telemetry, CancellationToken cancellationToken = default);

     Task<AircraftProfile?> GetByICAO24Async(string icao24, CancellationToken cancellationToken = default);

 
}
