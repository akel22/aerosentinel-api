

namespace AeroSentinel.Infrastructure.Implementations.Services;

public sealed class FlightTelemetryConsumerService : BackgroundService
{
    private readonly ChannelReader<SendTelemetryCommand> _reader;

    private readonly ILogger<FlightTelemetryConsumerService> _logger;

    private readonly IServiceScopeFactory _scopeFactory;

    public FlightTelemetryConsumerService(
        ChannelReader<SendTelemetryCommand> reader,
        IServiceScopeFactory scopeFactory,
        ILogger<FlightTelemetryConsumerService> logger)
    {
        _reader = reader;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var command in _reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await using var scope = _scopeFactory.CreateAsyncScope();

                var sender = scope.ServiceProvider.GetRequiredService<ISender>();

                await sender.Send(command, stoppingToken);

                _logger.LogInformation(
                    "Processed telemetry {ICAO24} sequence {Sequence}",
                    command.Payload.ICAO24,
                    command.Payload.Sequence);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "Failed to process telemetry for {ICAO24}",
                    command.Payload.ICAO24);
            }
        }
    }
}
