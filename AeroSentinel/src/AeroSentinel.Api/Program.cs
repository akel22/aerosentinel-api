var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Tell MediatR to scan your Application Class Library project for handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AeroSentinel.Application.Common.DTOs.RawPayloadDTO).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();

