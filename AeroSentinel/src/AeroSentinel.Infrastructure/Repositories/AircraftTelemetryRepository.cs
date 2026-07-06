namespace AeroSentinel.Infrastructure.Repositories;
public sealed class AircraftTelemetryRepository : IAircraftTelemetryRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public AircraftTelemetryRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;

    }
    public async Task<FlightTelemetry?> GetByCompositeIndexAsync(long sequence, Guid flightPlanId, CancellationToken cancellationToken = default)
    {
        var telemetry = await _applicationDbContext.flight_telemetry.
            FirstOrDefaultAsync(x => x.SequenceNumber == sequence && x.FlightPlanId == flightPlanId, cancellationToken);

        if(telemetry is null)
        {
            throw new InvalidMessageException(telemetry?.SequenceNumber, null, "Telemetry for this sequence is null");
        }

        return telemetry;    
    }

       public async Task SaveChangesAsync(FlightTelemetry telemetry, CancellationToken cancellationToken = default)
    {
       await _applicationDbContext.AddAsync(telemetry);

       await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<FlightTelemetry?> GetLatestFlightTelemetryAsync(string callsign, DateTime timestampUtc, CancellationToken cancellationToken = default)
    {
        var telemetry = await _applicationDbContext.flight_telemetry.
            FirstOrDefaultAsync(x => x.Callsign == callsign && x.TimestampUtc == timestampUtc, cancellationToken);

        if(telemetry is null)
        {
            throw new InvalidMessageException(telemetry?.SequenceNumber, null,  "Telemetry for this sequence is null");
        }

        return telemetry;    
    }

    public async Task<FlightTelemetry?> GetByMessageIdAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        var telemetry = await _applicationDbContext.flight_telemetry.FindAsync(messageId);

        if(telemetry is null)
        {
            throw new InvalidMessageException(null, telemetry?.MessageId, "Telemetry for this Message ID is null");
        }

        return telemetry;    
    }

    public async Task<IEnumerable<FlightTelemetry?>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.flight_telemetry
        .AsNoTracking().ToListAsync(cancellationToken);

    }

}