
using AeroSentinel.Api;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddLogging();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

// Add CORS to allow frontend requests
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() 
                     ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapHttpTelemetryEndpoints();
app.MapHttpFlightPlanEndpoints();
app.MapHttpDashboardEndpoints();

// await app.SeedWaypoints();

// app.MapPost("/credentials", async (
//     [FromBody] CredentialDTO credentialDTO,
//     [FromServices] IAircraftCredentialRepository repository,
//     CancellationToken cancellationToken) =>
// {
//     var credential = new AircraftCredential(credentialDTO.ICAO24);

//     await repository.SaveChangesAsync(credential, cancellationToken);

//     return Results.Ok();
// });

app.Run();

