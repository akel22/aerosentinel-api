namespace AeroSentinel.Application.AircraftTelemetry.Commands;

public record UpdateAircraftProfileCommand(AircraftProfileDTO AircraftProfileDTO) : IRequest<string>;
