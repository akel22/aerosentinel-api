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

            Console.WriteLine(path);
            Console.WriteLine(File.Exists(path));

            await importer.ImportAsync(path);

        }
    }      
}
