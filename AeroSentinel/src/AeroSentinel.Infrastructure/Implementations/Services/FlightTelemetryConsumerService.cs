

namespace AeroSentinel.Infrastructure.Implementations.Services;

public sealed class FlightTelemetryConsumerService : BackgroundService
{
    private readonly ChannelReader<SendTelemetryCommand> _reader;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<FlightTelemetryConsumerService> _logger;

    public FlightTelemetryConsumerService(
        ChannelReader<SendTelemetryCommand> reader,
        IServiceScopeFactory scopeFactory,
        ILogger<FlightTelemetryConsumerService> logger)
    {
        _reader = reader;
        _scopeFactory = scopeFactory;
        _logger = logger;
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
                    $"Processed telemetry {command.Payload.ICAO24} Sequence {command.Payload.Sequence}");
            }
            catch (DomainException e)
            {
                _logger.LogError(e, $"Failed processing telemetry for {command.Payload.ICAO24}");
            }
        }
    }
}
