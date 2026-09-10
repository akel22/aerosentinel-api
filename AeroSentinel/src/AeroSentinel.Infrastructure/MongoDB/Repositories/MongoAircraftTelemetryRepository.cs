namespace AeroSentinel.Infrastructure.Repositories;

using AeroSentinel.Domain.Entities;
using AeroSentinel.Domain.Enums;
using AeroSentinel.Domain.Exceptions;
using AeroSentinel.Domain.Repositories; // or IAircraftTelemetryRepository namespace
using MongoDB.Driver;

public sealed class MongoAircraftTelemetryRepository : IAircraftTelemetryRepository
{
    private readonly IMongoCollection<FlightTelemetry> _telemetryCollection;

    public MongoAircraftTelemetryRepository(IMongoDatabase database)
    {
        _telemetryCollection = database.GetCollection<FlightTelemetry>("flight_telemetry");
    }

    public async Task<FlightTelemetry?> GetByCompositeIndexAsync(
        long sequence, 
        Guid flightPlanId, 
        CancellationToken cancellationToken = default)
    {
        var telemetry = await _telemetryCollection
            .Find(x => x.SequenceNumber == sequence && x.FlightPlanId == flightPlanId)
            .FirstOrDefaultAsync(cancellationToken);

        if (telemetry is null)
        {
            throw new InvalidMessageException(sequence, null, "Telemetry for this sequence is null");
        }

        return telemetry;
    }

    public async Task SaveChangesAsync(
        FlightTelemetry telemetry, 
        CancellationToken cancellationToken = default)
    {
        // InsertOneAsync writes the new document directly to MongoDB
        await _telemetryCollection.InsertOneAsync(telemetry, cancellationToken: cancellationToken);
    }

    public async Task<FlightTelemetry?> GetLastFlightTelemetryAsync(
        string callsign, 
        DateTime timestampUtc, 
        CancellationToken cancellationToken = default)
    {
        var lastTelemetry = await _telemetryCollection
            .Find(x => x.Callsign == callsign && x.Status == TelemetryStatus.Finished)
            .SortByDescending(x => x.TimestampUtc)
            .FirstOrDefaultAsync(cancellationToken);

        return lastTelemetry;
    }

    public async Task<FlightTelemetry?> GetLastFlightTelemetryTimestampAsync(
        CancellationToken cancellationToken = default)
    {
        return await _telemetryCollection
            .Find(Builders<FlightTelemetry>.Filter.Empty)
            .SortByDescending(t => t.TimestampUtc)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<FlightTelemetry?> GetByMessageIdAsync(
        Guid messageId, 
        CancellationToken cancellationToken = default)
    {
        var telemetry = await _telemetryCollection
            .Find(x => x.MessageId == messageId)
            .FirstOrDefaultAsync(cancellationToken);

        if (telemetry is null)
        {
            throw new InvalidMessageException(null, messageId, "Telemetry for this Message ID is null");
        }

        return telemetry;
    }

    public async Task<IEnumerable<FlightTelemetry?>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _telemetryCollection
            .Find(Builders<FlightTelemetry>.Filter.Empty)
            .ToListAsync(cancellationToken);

        return result;
    }
}