namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class TelemetryConfiguration : IEntityTypeConfiguration<FlightTelemetry>
    {
        public void Configure(EntityTypeBuilder<FlightTelemetry> builder)
        {
           builder.HasKey(
                x => x.MessageId);

                builder.HasIndex(
                x => new
                {
                    x.FlightPlanID,
                    x.SequenceNumber
                })
                .IsUnique();

                builder.HasIndex(
                x => new
                {
                    x.Callsign,
                    x.TimestampUtc
                });
         
        }
    }
}