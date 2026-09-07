public static class MapEndpoints
{
     public static void MapHttpTelemetryEndpoints(this WebApplication app)
    {
        var telemetry = app.MapGroup("/telemetry");

        const string routeName = "GetTelemetry";

        // POST REQUEST
        telemetry.MapPost("/", async (
            [FromBody] RawPayloadDTO payload,
            [FromServices]ChannelWriter<SendTelemetryCommand> writer,
            [FromServices]ILogger<WebApplication> logger,
            CancellationToken cancellationToken) =>
        {
            logger.LogInformation(
            "Endpoint: Lat={Lat}, Lon={Lon}",
            payload.Latitude,
            payload.Longitude);

            var command = new SendTelemetryCommand(payload);

            try
            {
                await writer.WriteAsync(command, cancellationToken);

            }
            catch
            {
                return Results.BadRequest();
            }

            return Results.Accepted();
            
     
        });

        // GET ALL
        telemetry.MapGet("/", async (
            [FromServices] IAircraftTelemetryRepository aircraftTelemetryRepository,
            CancellationToken cancellationToken) =>
        {
            var telemetryFrames = await aircraftTelemetryRepository.GetAllAsync(cancellationToken);

            return Results.Ok(telemetryFrames);
        });

        // GET BY MESSAGE ID
        telemetry.MapGet("/{messageId:guid}", async (
            Guid messageId,
            [FromServices] IAircraftTelemetryRepository aircraftTelemetryRepository,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var telemetry = await aircraftTelemetryRepository.GetByMessageIdAsync(messageId);

                return Results.Ok(telemetry);
            }
            catch (DomainException exception)
            {
                return Results.NotFound(new
                {
                    error = exception.Message
                });
            }
        })
        .WithName(routeName);
    }
}
