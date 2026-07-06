using AeroSentinel.Application;
using AeroSentinel.Domain.Entities;
using AeroSentinel.Infrastructure;
using AeroSentinel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddLogging();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.MapHttpProfileEndpoints();
app.MapHttpTelemetryEndpoints();

app.MapPost("/credentials", async ([FromBody]CredentialDTO credentialDTO, 
                            [FromServices]IAircraftCredentialRepository repository,
                            CancellationToken cancellationToken)=>
{
    var credential = new AircraftCredential(credentialDTO.ICAO24);

    await repository.SaveChangesAsync(credential, cancellationToken);

    return Results.Ok();
});



app.Run();

