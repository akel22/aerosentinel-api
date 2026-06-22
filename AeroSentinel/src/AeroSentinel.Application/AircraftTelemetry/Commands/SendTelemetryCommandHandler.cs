using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using AeroSentinel.Application.Security;
using AeroSentinel.Application.Common.Interfaces; // Assuming repositories live here
using AeroSentinel.Domain.Entities;
using AeroSentinel.Domain.ValueObjects;
using AeroSentinel.Domain.Exceptions;

namespace AeroSentinel.Application.AircraftTelemetry.Commands;

public class SendTelemetryCommandHandler : IRequestHandler<SendTelemetryCommand, Guid>
{
    private readonly IAircraftProfileRepository _aircraftProfileRepository;
    private readonly IAircraftTelemetryRepository _aircraftTelemetryRepository;
    private readonly IReplayProtectionService _replayProtectionService; // Fixed: Swapped to abstract interface
    private readonly ILogger<SendTelemetryCommandHandler> _logger; // Fixed: Swapped to ILogger interface

    public SendTelemetryCommandHandler( 
        IAircraftProfileRepository aircraftProfileRepository,
        IAircraftTelemetryRepository aircraftTelemetryRepository,
        IReplayProtectionService replayProtectionService,
        ILogger<SendTelemetryCommandHandler> logger)
    {
        _aircraftProfileRepository = aircraftProfileRepository;
        _aircraftTelemetryRepository = aircraftTelemetryRepository;
        _replayProtectionService = replayProtectionService;
        _logger = logger;
    }

    public async Task<Guid> Handle(SendTelemetryCommand request, CancellationToken cancellationToken)
    {        
        var rawPayload = request.Payload;

        // 1. Group structural metrics into highly cohesive Value Objects
        var coordinates = new AircraftGeoCoordinates(rawPayload.Latitude, rawPayload.Longitude);
        var spatialState = new SpatialState(
            coordinates,
            rawPayload.BaroAltitudeFeet,
            rawPayload.GeoAltitudeFeet,
            rawPayload.GroundSpeedKnots,
            rawPayload.TrackAngleDegrees
        );

        var flightIntent = new FlightIntent(
            rawPayload.VerticalRateFpm,
            rawPayload.SelectedAltitudeFeet,
            rawPayload.IndicatedAirspeedKnots,
            rawPayload.MagneticHeadingDegrees, 
            rawPayload.RollAngleDegrees
        );

        // 2. Query for historical telemetry baseline
        var recentTelemetry = await _aircraftTelemetryRepository.GetByICAO24Async(rawPayload.ICAO24!, cancellationToken);

        // Fixed: Extract the last sequence safely without crashing if it's a first-time stream connection
        long? lastAcceptedSequence = recentTelemetry?.SequenceNumber; 

        // 3. Execute the security layer verification pipeline
        _replayProtectionService.ValidateSequence(
            rawPayload.TimestampUTC, 
            rawPayload.Sequence,
            lastAcceptedSequence
        );

        // 4. Instantiate the domain aggregate root (Runs your internal guard clauses)
        var flightTelemetry = new FlightTelemetry(
            rawPayload.ICAO24!,
            rawPayload.Callsign!,
            rawPayload.Squawk!,
            rawPayload.TimestampUTC,
            spatialState,
            flightIntent,
            rawPayload.Sequence,
            rawPayload.Signature
        );

        // 5. Fixed: Actively persist the aggregate state to your data store repository
        await _aircraftTelemetryRepository.SaveAsync(flightTelemetry, cancellationToken);
        
        _logger.LogInformation("Successfully tracked telemetry frame for ICAO24: {ICAO24}", rawPayload.ICAO24);

        return flightTelemetry.MessageId;
    }
}