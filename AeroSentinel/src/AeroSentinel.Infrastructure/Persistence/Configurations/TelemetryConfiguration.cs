namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class TelemetryConfiguration : IEntityTypeConfiguration<FlightTelemetry>
    {
        public void Configure(EntityTypeBuilder<FlightTelemetry> builder)
        {
           builder.HasKey(x => x.MessageId);

           builder.HasIndex(x => new
                {
                    x.FlightPlanId,
                    x.SequenceNumber

                }).IsUnique();

           builder.HasIndex(x => new 
                {
                    x.Callsign,
                    x.TimestampUtc
                });

            builder.Property(x => x.Status).HasConversion<string>().IsRequired();

            builder.OwnsOne(x => x.FlightIntent, _ =>
                {
                    _.Property(x => x.VerticalRateFpm);
                    _.Property(x => x.SelectedAltitudeFeet);
                    _.Property(x => x.IndicatedAirspeedKnots);
                    _.Property(x => x.MagneticHeadingDegrees);
                    _.Property(x => x.RollAngleDegrees);
                    
                });

            builder.OwnsOne(x => x.SpatialState, _ =>
                {
                    _.Property(x => x.Coordinates);
                    _.Property(x => x.BaroAltitudeFeet);
                    _.Property(x => x.GeoAltitudeFeet);
                    _.Property(x => x.GroundSpeedKnots);
                    _.Property(x => x.TrackAngleDegrees);
                    
                });

             builder.HasOne(x => x.FlightPlan).WithMany().HasForeignKey(x => x.FlightPlanId);

        }
    }
}