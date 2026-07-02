namespace AeroSentinel.Application.AircraftTelemetry.Commands;
public record SendTelemetryCommand(RawPayloadDTO Payload) : IRequest<Guid>;