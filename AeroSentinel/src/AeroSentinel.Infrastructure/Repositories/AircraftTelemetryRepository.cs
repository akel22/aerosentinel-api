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

    public async Task<FlightTelemetry?> GetByCompositeIndexAsync(long sequence, Guid flightPlanId, CancellationToken cancellationToken = default)
    {
        var telemetry = await _applicationDbContext.FlightTelemetries.
            FirstOrDefaultAsync(x => x.SequenceNumber == sequence && x.FlightPlanID == flightPlanId, cancellationToken);

        return telemetry;    
    }
}