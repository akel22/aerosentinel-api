public static class MapEndpoints
{
    public static void MapHttpProfileEndpoints(this WebApplication app)
    {

        // //POST REQUEST 
        // var profiles = app.MapGroup("/profiles");
        // const string routeName = "getRoute";

        // profiles.MapPost("/", async (
        //     [FromBody] AircraftProfileDTO aircraftProfileDTO,
        //     [FromServices] ISender sender,
        //     CancellationToken cancellationToken) =>
        // {
        //     var command = new CreateAircraftProfileCommand(aircraftProfileDTO);

        //     try
        //     {
        //         var icao24 = await sender.Send(command, cancellationToken);
                
        //         return Results.CreatedAtRoute(routeName, new { icao24 }, aircraftProfileDTO);
        //     }
        //     catch (DomainException exception)
        //     {
        //         return Results.BadRequest(new { error = exception.Message });
        //     }
           
        // });

        // //GET REQUEST
        // profiles.MapGet("/", async (
        //     [FromServices] IAircraftProfileRepository aircraftProfileRepository,
        //     CancellationToken cancellationToken) =>
        // {
        //     var aircraftProfiles = await aircraftProfileRepository.GetAllAsync(cancellationToken);

        //     return Results.Ok(aircraftProfiles);
        // });

        // //GET REQUEST BY ICAO24
        // profiles.MapGet("/{icao24}", async (
        //     string icao24,
        //     [FromServices] IAircraftProfileRepository aircraftProfileRepository,
        //     CancellationToken cancellationToken) =>
        // {
        //     try
        //     {
        //         var aircraftProfile = await aircraftProfileRepository.GetByICAO24Async(icao24, cancellationToken);

        //         return Results.Ok(aircraftProfile);
        //     }
        //     catch (InvalidAircraftIdentifierException)
        //     {
        //         return Results.NotFound();
        //     }
        // }).WithName(routeName);

        // //PUT REQUEST
        // profiles.MapPut("/{icao24}", async (
        //     string icao24,
        //     [FromBody] AircraftProfileDTO aircraftProfileDTO,
        //     [FromServices] ISender sender,
        //     CancellationToken cancellationToken) =>
        // {
        //     if (!string.Equals(icao24, aircraftProfileDTO.ICAO24, StringComparison.OrdinalIgnoreCase))
        //     {
        //         return Results.BadRequest("Route ICAO24 must match the request body ICAO24.");
        //     }

        //     try
        //     {
        //         var command = new UpdateAircraftProfileCommand(aircraftProfileDTO);
        //         await sender.Send(command, cancellationToken);

        //         return Results.NoContent();
        //     }
        //     catch (InvalidAircraftIdentifierException)
        //     {
        //         return Results.NotFound();
        //     }
        //     catch
        //     {
        //         return Results.BadRequest();
        //     }
        // });


    }

     public static void MapHttpTelemetryEndpoints(this WebApplication app)
    {
        var telemetry = app.MapGroup("/telemetry");

        const string routeName = "GetTelemetry";

        // POST REQUEST
        telemetry.MapPost("/", async (
            [FromBody] RawPayloadDTO payload,
            ChannelWriter<SendTelemetryCommand> writer,
            CancellationToken cancellationToken) =>
        {
            var command = new SendTelemetryCommand(payload);

            await writer.WriteAsync(command, cancellationToken);

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
