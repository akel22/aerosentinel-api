namespace AeroSentinel.Infrastructure.Repositories;
public sealed class AircraftTelemetryRepository : IAircraftTelemetryRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public AircraftTelemetryRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task SaveAsync(FlightTelemetry telemetry, CancellationToken cancellationToken = default)
    {
       await _applicationDbContext.AddAsync(telemetry);

       await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<FlightTelemetry?> GetByMessageCompositeIndexAsync(long sequence, string callsign, CancellationToken cancellationToken = default)
    {

        var telemetry = _applicationDbContext.FlightTelemetries.
            FirstOrDefaultAsync(f => f.SequenceNumber == sequence and  f.Callsign == callsign);
    }

}