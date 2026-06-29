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
    private readonly IAircraftTelemetryRepository _aircraftTelemetryRepository;
    private readonly  ICryptographyService _cryptoService;
    private readonly IReplayProtectionService _replayProtectionService; 
    private readonly ILogger<SendTelemetryCommandHandler> _logger; 

    public SendTelemetryCommandHandler( 
        IAircraftTelemetryRepository aircraftTelemetryRepository,
        ICryptographyService cryptoService,
        IReplayProtectionService replayProtectionService,
        ILogger<SendTelemetryCommandHandler> logger)
    {
        _aircraftTelemetryRepository = aircraftTelemetryRepository;
        _cryptoService = cryptoService;
        _replayProtectionService = replayProtectionService;
        _logger = logger;
    }

    public async Task<Guid> Handle(SendTelemetryCommand request, CancellationToken cancellationToken)
    {        
        var payload = request.Payload;

        var coordinates = new AircraftGeoCoordinates(payload.Latitude, payload.Longitude);
        var spatialState = new SpatialState(
            coordinates,
            payload.BaroAltitudeFeet,
            payload.GeoAltitudeFeet,
            payload.GroundSpeedKnots,
            payload.TrackAngleDegrees
        );

        var flightIntent = new FlightIntent(
            payload.VerticalRateFpm,
            payload.SelectedAltitudeFeet,
            payload.IndicatedAirspeedKnots,
            payload.MagneticHeadingDegrees, 
            payload.RollAngleDegrees
        );



        var recentTelemetry = await _aircraftTelemetryRepository.GetByCompositeIndexAsync(payload.Sequence, 
        payload.Callsign);

        long? lastAcceptedSequence = recentTelemetry?.SequenceNumber; 

        _replayProtectionService.ValidateSequence(
            payload.TimestampUTC, 
            payload.Sequence,
            lastAcceptedSequence
        );

        var flightTelemetry = new FlightTelemetry(
            payload.FlightPlanId,
            payload.ICAO24!,
            payload.Callsign!,
            payload.Squawk!,
            payload.TimestampUTC,
            spatialState,
            flightIntent,
            payload.Sequence,
            payload.Signature
        );

        // 5. Fixed: Actively persist the aggregate state to your data store repository
        await _aircraftTelemetryRepository.SaveAsync(flightTelemetry, cancellationToken);
        
        _logger.LogInformation("Successfully tracked telemetry frame for ICAO24: {ICAO24}", payload.ICAO24);

        return flightTelemetry.MessageId;
    }
}