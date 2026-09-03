namespace AeroSentinel.Application.AircraftTelemetry.Commands;

public record ReadTelemetryCommand : IRequest<DashboardSnapshotDTO>;