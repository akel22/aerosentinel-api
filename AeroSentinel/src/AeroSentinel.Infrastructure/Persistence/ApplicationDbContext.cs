namespace AeroSentinel.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {     
        }
        public DbSet<FlightTelemetry> FlightTelemetries {get; set;}

        public DbSet<AircraftCredential> AircraftCredentials {get; set;}
        public DbSet<Waypoint> Waypoints { get; set; }

    }
}