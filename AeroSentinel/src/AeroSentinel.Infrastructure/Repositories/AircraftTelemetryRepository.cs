using AeroSentinel.Application.Common.Interfaces;

public sealed class AircraftTelemetryRepository : IAircraftTelemetryRepository
{
    public Task SaveAsync(FlightTelemetry telemetry, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public Task<FlightTelemetry?> GetByICAO24Async(string icao24, 
                    CancellationToken cancellationToken = default)
    {
        return null;
    }

}