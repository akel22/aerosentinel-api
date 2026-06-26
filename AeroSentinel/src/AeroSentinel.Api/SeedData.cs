using AeroSentinel.Infrastructure.Importing;

public static class SeedData
{

     public static async Task SeedWaypoints(this WebApplication app)
    {
            using (var scope = app.Services.CreateScope())
        {   
            var importer = scope.ServiceProvider.GetRequiredService<WaypointsCSVService>();

            var path = Path.Combine(AppContext.BaseDirectory,"SeedData",
            "PhilippineAircraftWaypoints_Final.csv");

            await importer.ImportAsync(path);

        }
    }      
}
