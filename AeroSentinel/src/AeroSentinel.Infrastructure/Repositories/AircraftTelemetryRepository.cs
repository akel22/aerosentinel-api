using AeroSentinel.Application.Common.Interfaces;

public sealed class AircraftTelemetryRepository : IAircraftTelemetryRepository
{
    public async Task SaveAsync(FlightTelemetry telemetry, CancellationToken cancellationToken = default)
    {
       
    }

    public async Task<FlightTelemetry?> GetByICAO24Async(string icao24, 
    CancellationToken cancellationToken = default)
    {
        return null;
    }

}