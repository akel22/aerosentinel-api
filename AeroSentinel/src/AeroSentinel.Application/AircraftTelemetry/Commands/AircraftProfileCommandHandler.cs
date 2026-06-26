using AeroSentinel.Application.AircraftTelemetry.Commands;
using Microsoft.Extensions.Logging;

public sealed class AircraftProfileCommandHandler : IRequestHandler<CreateAircraftProfileCommand, string>
{
    IAircraftProfileRepository _aircraftProfileRepository;
    ILogger _logger;

    public AircraftProfileCommandHandler(IAircraftProfileRepository aircraftProfileRepository,
    ILogger logger){

        _aircraftProfileRepository = aircraftProfileRepository;
        _logger = logger;
    }

    public async Task<string> Handle(CreateAircraftProfileCommand request, CancellationToken cancellation = default)
    {
     var newAircraftProfile = request.AircraftProfileDTO;

     var newPerformance = new AircraftPerformance(
        newAircraftProfile.CruiseSpeedKnots,
        newAircraftProfile.MaxVelocityKnots
     );

     var aircraftProfile = new AircraftProfile(
        newAircraftProfile.ICAO24,
        newAircraftProfile.Registration,
        newAircraftProfile.AircraftType,
        newPerformance,
        newAircraftProfile.CredentialId,
        newAircraftProfile.MaxAltitudeFeet,
        newAircraftProfile.MaxClimbRateFeetPerMinute,
        newAircraftProfile.MaxDescentRateFeetPerMinute,
        newAircraftProfile.MaxTurnRateDegreesPerSecond
     );

        await _aircraftProfileRepository.SaveProfileAsync(aircraftProfile, cancellation);
        _logger.LogInformation($"New aircraft profile has been created at {DateTime.UtcNow}");


        return aircraftProfile.ICAO24;
    }
}