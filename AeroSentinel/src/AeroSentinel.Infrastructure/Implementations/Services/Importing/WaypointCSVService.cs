namespace AeroSentinel.Infrastructure.Importing
{
    public sealed class WaypointsCSVService
    {
        private readonly ApplicationDbContext _dbContext;

        public WaypointsCSVService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ImportAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var rows = csv.GetRecords<WaypointsCSV>().ToList();

            foreach (var row in rows)
            {
                var exists = await _dbContext.Waypoints.AnyAsync(x => x.WaypointId == row.WaypointId);

                if (exists) continue;

                var waypoint = new Waypoint(row.WaypointId, row.Latitude, row.Longitude);

                _dbContext.Waypoints.Add(waypoint);
            }

            await _dbContext.SaveChangesAsync();

        }

        public async  Task SeedWaypoints(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
        {   
            var importer =
                scope.ServiceProvider.GetRequiredService<WaypointsCSVService>();

            await importer.ImportAsync( @"C:\Waypoints.csv");

        }
    }
}
}