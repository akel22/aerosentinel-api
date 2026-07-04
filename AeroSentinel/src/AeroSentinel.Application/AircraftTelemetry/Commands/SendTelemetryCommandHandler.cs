namespace AeroSentinel.Application.AircraftTelemetry.Commands;

public class SendTelemetryCommandHandler : IRequestHandler<SendTelemetryCommand, Guid>
{
    private readonly IAircraftTelemetryRepository _aircraftTelemetryRepository;
    private readonly ICryptographyService _cryptoService;

    private readonly IAircraftCredentialCacheService _aircraftCredentialCacheService;
    private readonly IReplayProtectionService _replayProtectionService;
    private readonly ILogger<SendTelemetryCommandHandler> _logger;

    public SendTelemetryCommandHandler(
        IAircraftTelemetryRepository aircraftTelemetryRepository,
        ICryptographyService cryptoService,
        IAircraftCredentialCacheService aircraftCredentialCacheService,
        IReplayProtectionService replayProtectionService,
        ILogger<SendTelemetryCommandHandler> logger)
    {
        _aircraftTelemetryRepository = aircraftTelemetryRepository;
        _cryptoService = cryptoService;
        _aircraftCredentialCacheService = aircraftCredentialCacheService;
        _replayProtectionService = replayProtectionService;
        _logger = logger;
    }

    public async Task<Guid> Handle(SendTelemetryCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;

        var sharedKey = await _aircraftCredentialCacheService.GetSharedVerificationAsync(payload.ICAO24,
        cancellationToken);

        if (sharedKey is null) throw new InvalidVerificationKeyException(sharedKey, "Credential cannot be found");

        var isValid = _cryptoService.VerifyPayloadSignature(payload, sharedKey);

        if(!isValid)
        {
            throw new SignatureException(payload.Signature, "The incoming telemetry is spoofed");
        }
        var recentTelemetry = await _aircraftTelemetryRepository.GetLatestFlightTelemetryAsync(
            payload.Callsign,
            payload.TimestampUTC, cancellationToken);

        long? lastAcceptedSequence = recentTelemetry?.SequenceNumber;

        _replayProtectionService.ValidateSequence(
            payload.TimestampUTC,
            payload.Sequence,
            lastAcceptedSequence);

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

        flightTelemetry.MarkAsVerified();

        // 5. Fixed: Actively persist the aggregate state to your data store repository
        await _aircraftTelemetryRepository.SaveChangesAsync(flightTelemetry, cancellationToken);

        _logger.LogInformation("Telemetry accepted for {ICAO24} Sequence {Sequence}", payload.ICAO24, payload.Sequence);

        return flightTelemetry.MessageId;
    }
}