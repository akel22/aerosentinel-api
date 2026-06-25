public class ApplicationDbContext: DbContext
{
    public DbSet<Waypoint> Waypoints{get; set;}


}