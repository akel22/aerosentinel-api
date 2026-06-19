
namespace AeroSentinel.Application.AircraftTelemetry.Commands;

public class SendTelemetryCommandHandler : IRequestHandler<SendTelemetryCommand, Guid>
{
    private readonly IAircraftCredentialRepository _aircraftCredentialRepository;
    private readonly IAircraftProfileRepository _aircraftProfileRepository;
    private readonly IAircraftTelemetryRepository _aircraftTelemetryRepository;
    public SendTelemetryCommandHandler(IAircraftCredentialRepository aircraftCredentialRepository,
            IAircraftProfileRepository aircraftProfileRepository,
            IAircraftTelemetryRepository aircraftTelemetryRepository)
    {
        _aircraftCredentialRepository = aircraftCredentialRepository;
        _aircraftProfileRepository = aircraftProfileRepository;
        _aircraftTelemetryRepository = aircraftTelemetryRepository;
    }

    public async Task<Guid> Handle(SendTelemetryCommand command, CancellationToken cancellationToken)
    {
       var rawPayload = command.Payload;

       await _aircraftCredentialRepository.GetByICAO24Async(rawPayload.Icao24);

       var telemetry = new FlightTelemetry(
        rawPayload.Icao24,
        rawPayload.Callsign,
        rawPayload.Timestamp,
        rawPayload.Latitude,
        rawPayload.Longitude,
        rawPayload.Altitude,
        rawPayload.GroundSpeedKnots,
        rawPayload.TrackDegrees,
        rawPayload.VerticalRatePerMinute

       );

      
    }
}