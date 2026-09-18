using System.Security.Cryptography;
using AeroSentinel.Domain.Extensions; // adjust to match where AircraftCredentialException / DomainException actually live

namespace AeroSentinel.Api;
    public sealed record CreateAircraftCredentialRequest(string ICAO24);

    public sealed record CreateAircraftCredentialResponse(string ICAO24, string VerificationKey);

    public static class AircraftCredentialEndpoints
    {
        public static void MapAircraftCredentialEndpoints(this WebApplication app)
        {
            var credentials = app.MapGroup("/credentials");

            // POST /credentials — issue a new credential for an aircraft
            credentials.MapPost("/", async (
                CreateAircraftCredentialRequest request,
                [FromServices] IAircraftCredentialRepository credentialRepository,
                CancellationToken cancellationToken) =>
            {
                // 256-bit key — correct size for HMAC-SHA256
                var rawBytesKey = RandomNumberGenerator.GetBytes(32);
                var hexVerificationKey = Convert.ToHexString(rawBytesKey);

                var credentialDto = new CredentialDTO(
                    CredentialId: Guid.Empty,
                    ICAO24: request.ICAO24,
                    VerificationKey: hexVerificationKey,
                    Status: CredentialStatus.Active,
                    CreatedUtc: DateTime.UtcNow);

                try
                {
                    await credentialRepository.SaveChangesAsync(credentialDto, cancellationToken);
                }
                catch (AircraftCredentialException exception)
                {
                    // thrown when a credential for this ICAO24 already exists
                    return Results.Conflict(new { error = exception.Message });
                }
                catch (DomainException exception)
                {
                    // thrown by validation (e.g. malformed ICAO24)
                    return Results.BadRequest(new { error = exception.Message });
                }

                return Results.Created();
            })
            .WithName("CreateAircraftCredential");

            // GET /credentials — list credentials (metadata only, never the raw key)
            credentials.MapGet("/", async (
                [FromServices] IAircraftCredentialRepository credentialRepository,
                CancellationToken cancellationToken) =>
            {
                var aircraftCredentials = await credentialRepository.GetAllAsync(cancellationToken);
                
                var aircraftCredentialReadList = aircraftCredentials.Select(x => new
                {
                    x.CredentialId,
                    x.ICAO24,
                    x.Status,
                    x.VerificationKey,
                    x.CreatedUtc
                });

                return Results.Ok(aircraftCredentialReadList);
            })
            .WithName("GetAllAircraftCredentials");
        }
}