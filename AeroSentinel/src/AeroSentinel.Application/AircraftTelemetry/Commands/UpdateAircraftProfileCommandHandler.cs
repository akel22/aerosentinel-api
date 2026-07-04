using Microsoft.Extensions.Logging;

namespace AeroSentinel.Application.AircraftTelemetry.Commands;

public sealed class UpdateAircraftProfileCommandHandler : IRequestHandler<UpdateAircraftProfileCommand, string>
{
    private readonly IAircraftProfileRepository _aircraftProfileRepository;
    private readonly ILogger<UpdateAircraftProfileCommandHandler> _logger;

    public UpdateAircraftProfileCommandHandler(
        IAircraftProfileRepository aircraftProfileRepository,
        ILogger<UpdateAircraftProfileCommandHandler> logger)
    {
        _aircraftProfileRepository = aircraftProfileRepository;
        _logger = logger;
    }

    public async Task<string> Handle(UpdateAircraftProfileCommand request, CancellationToken cancellationToken = default)
    {
        var updatedAircraftProfile = request.AircraftProfileDTO;

        var updatedPerformance = new AircraftPerformance(
            updatedAircraftProfile.CruiseSpeedKnots,
            updatedAircraftProfile.MaxVelocityKnots,
            updatedAircraftProfile.MaxAltitudeFeet,
            updatedAircraftProfile.MaxClimbRateFeetPerMinute,
            updatedAircraftProfile.MaxDescentRateFeetPerMinute,
            updatedAircraftProfile.MaxTurnRateDegreesPerSecond);

        var aircraftProfile = new AircraftProfile(
            updatedAircraftProfile.ICAO24,
            updatedAircraftProfile.Registration,
            updatedAircraftProfile.AircraftType,
            updatedPerformance);

        await _aircraftProfileRepository.UpdateAsync(aircraftProfile, cancellationToken);

        _logger.LogInformation("Aircraft profile {ICAO24} has been updated at {UpdatedAt}", aircraftProfile.ICAO24, DateTime.UtcNow);

        return aircraftProfile.ICAO24;
    }
}
