using AeroSentinel.Application.AircraftTelemetry.Commands;
using AeroSentinel.Application.Common.DTOs;
using AeroSentinel.Application.Common.Interfaces;
using AeroSentinel.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public static class MapEndpoints
{
    public static void MapHttpProfileEndpoints(this WebApplication app)
    {

        //POST REQUEST 
        var profiles = app.MapGroup("/profiles");
        const string routeName = "getRoute";

        profiles.MapPost("/", async (
            [FromBody] AircraftProfileDTO aircraftProfileDTO,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateAircraftProfileCommand(aircraftProfileDTO);

            try
            {
                var icao24 = await sender.Send(command, cancellationToken);
                
                return Results.CreatedAtRoute(routeName, icao24, aircraftProfileDTO);
            }
            catch
            {
                return Results.BadRequest();
            }
        });

        //GET REQUEST
        profiles.MapGet("/", async (
            [FromServices] IAircraftProfileRepository aircraftProfileRepository,
            CancellationToken cancellationToken) =>
        {
            var aircraftProfiles = await aircraftProfileRepository.GetAllAsync(cancellationToken);

            return Results.Ok(aircraftProfiles);
        });

        //GET REQUEST BY ICAO24
        profiles.MapGet("/{icao24}", async (
            string icao24,
            [FromServices] IAircraftProfileRepository aircraftProfileRepository,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var aircraftProfile = await aircraftProfileRepository.GetByICAO24Async(icao24, cancellationToken);

                return Results.Ok(aircraftProfile);
            }
            catch (InvalidAircraftIdentifierException)
            {
                return Results.NotFound();
            }
        }).WithName(routeName);

        //PUT REQUEST
        profiles.MapPut("/{icao24}", async (
            string icao24,
            [FromBody] AircraftProfileDTO aircraftProfileDTO,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            if (!string.Equals(icao24, aircraftProfileDTO.ICAO24, StringComparison.OrdinalIgnoreCase))
            {
                return Results.BadRequest("Route ICAO24 must match the request body ICAO24.");
            }

            try
            {
                var command = new UpdateAircraftProfileCommand(aircraftProfileDTO);
                await sender.Send(command, cancellationToken);

                return Results.NoContent();
            }
            catch (InvalidAircraftIdentifierException)
            {
                return Results.NotFound();
            }
            catch
            {
                return Results.BadRequest();
            }
        });


    }
}
