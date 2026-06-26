namespace AeroSentinel.Application.AircraftTelemetry.Commands
{
    public record CreateAircraftProfileCommand(AircraftProfileDTO AircraftProfileDTO) : IRequest<string>;
}