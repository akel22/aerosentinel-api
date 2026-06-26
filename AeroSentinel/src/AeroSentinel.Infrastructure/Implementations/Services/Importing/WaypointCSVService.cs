namespace AeroSentinel.Infrastructure.Importing;

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

        var rows = await csv.GetRecordsAsync<WaypointsCSV>().ToListAsync();

        var existing = await _dbContext.Waypoints.Select(x => x.WaypointId).ToHashSetAsync();

        var batch = new List<Waypoint>();

        foreach(var row in rows)
        {
            if(existing.Contains(row.WaypointId)) continue;
          
            batch.Add(new Waypoint(row.WaypointId, row.Latitude, row.Longitude));
        }

        if(batch.Count>0)
        {
            await _dbContext.Waypoints.AddRangeAsync(batch);

            await _dbContext.SaveChangesAsync();
        }
    }
}