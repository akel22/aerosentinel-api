namespace AeroSentinel.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {     
        }
        // //POSTGRE NAMING CONVENTION
        // public DbSet<FlightTelemetry> flight_telemetry {get; set;}
        
        // public DbSet<AircraftCredential> aircraft_credential {get; set;}
        public DbSet<AircraftProfile> aircraft_profile {get; set;}
        public DbSet<Waypoint> waypoint { get; set; }
        
        // public DbSet<FlightPlan> flight_plan { get; set; }
        // public DbSet<FlightPlanRoute> flight_plan_route { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            
        }
    }   
}