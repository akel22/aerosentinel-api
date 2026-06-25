namespace AeroSentinel.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {     
        }
        
        public DbSet<Waypoint> Waypoints { get; set; }

    }
}