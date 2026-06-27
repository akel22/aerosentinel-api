namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class TelemetryConfiguration : IEntityTypeConfiguration<FlightTelemetry>
    {
        public void Configure(EntityTypeBuilder<FlightTelemetry> builder)
        {
            builder.HasKey(x => new
            {
                x.MessageId
                
            });

            builder.HasIndex(x => new
            {
                x.SequenceNumber,
                x.Callsign

            });
         
        }
    }
}