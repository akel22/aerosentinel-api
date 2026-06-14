namespace AeroSentinel.Application.Telemetry.Commands;

/// <summary>
/// CQRS Command wrapper that routes the streaming ingestion payload into the application pipeline.
/// </summary>
public record SendTelemetryCommand(RawPayloadDTO Payload) : IRequest<Guid>;