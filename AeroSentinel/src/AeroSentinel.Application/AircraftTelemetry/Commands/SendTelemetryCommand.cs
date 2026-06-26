namespace AeroSentinel.Application.Telemetry.Commands;
public record SendTelemetryCommand(RawPayloadDTO Payload) : IRequest<Guid>;