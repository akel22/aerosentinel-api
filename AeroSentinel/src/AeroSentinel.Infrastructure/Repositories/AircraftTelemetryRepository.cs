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

    public async Task<FlightTelemetry?> GetBySequenceAsync(long sequence, 
    CancellationToken cancellationToken = default)
    {
        //GET BY SEQUENCE NUMBER AND DATETIME COMPOSITE INDEX



        return await null;
    }

}