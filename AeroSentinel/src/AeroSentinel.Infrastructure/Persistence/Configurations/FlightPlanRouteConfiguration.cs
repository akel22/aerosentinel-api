namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class FlightPlanRouteConfiguration : IEntityTypeConfiguration<FlightPlanRoute>
    {
        public void Configure(EntityTypeBuilder<FlightPlanRoute> builder)
        {
           builder.HasKey(x => new
           {
            x.FlightPlanId,
            x.Sequence

           });
           

           builder.HasOne(x => x.FlightPlan).WithMany().HasForeignKey(x => x.FlightPlanId);
                  
           builder.HasOne(x => x.Waypoint).WithMany().HasForeignKey(x => x.WaypointId);
                 
            
                    
                
        }
    }
}