namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class FlightPlanConfiguration : IEntityTypeConfiguration<FlightPlan>
    {
        public void Configure(EntityTypeBuilder<FlightPlan> builder)
        {
           builder.HasKey(x => x.FlightPlanId);
                    
        }
    }
}