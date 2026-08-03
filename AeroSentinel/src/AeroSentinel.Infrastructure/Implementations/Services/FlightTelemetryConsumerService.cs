

using Microsoft.AspNetCore.Mvc;

namespace AeroSentinel.Infrastructure.Implementations.Services;

public sealed class FlightTelemetryConsumerService : BackgroundService
{
    private readonly ChannelReader<SendTelemetryCommand> _reader;

    private readonly ILogger<FlightTelemetryConsumerService> _logger;

     private readonly ISender _sender;

    public FlightTelemetryConsumerService(
        [FromServices] ISender sender,
        ChannelReader<SendTelemetryCommand> reader,
        IServiceScopeFactory scopeFactory,
        ILogger<FlightTelemetryConsumerService> logger)
    {
        _reader = reader;
        _logger = logger;
        _sender = sender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var command in _reader.ReadAllAsync(stoppingToken))
        {
            try
            {       
                await _sender.Send(command, stoppingToken);

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
