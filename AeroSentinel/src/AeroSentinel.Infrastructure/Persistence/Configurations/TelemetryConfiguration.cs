namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class TelemetryConfiguration : IEntityTypeConfiguration<FlightTelemetry>
    {
        public void Configure(EntityTypeBuilder<FlightTelemetry> builder)
        {
            builder.HasKey(x => new
            {
                x.FlightPlanID,
                x.TimestampUtc,
                x.SequenceNumber
                
            });

            builder.HasIndex(x => new
            {
                x.SequenceNumber,
                x.TimestampUtc
            });
         
        }
    }
}