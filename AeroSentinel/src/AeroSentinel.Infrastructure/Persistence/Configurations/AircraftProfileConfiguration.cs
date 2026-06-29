namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class AircraftProfileConfiguration : IEntityTypeConfiguration<AircraftProfile>
    {
        public void Configure(EntityTypeBuilder<AircraftProfile> builder)
        {
           builder.HasKey(
                x => x.ICAO24);

            builder.OwnsOne(x => x.AircraftPerformance, AircraftPerformance =>
            {
                AircraftPerformance.Property(x => x.CruiseSpeedKnots).HasColumnName("CruiseSpeedKnots");
                AircraftPerformance.Property(x => x.MaxVelocityKnots).HasColumnName("MaxVelocityKnots");
            });

            builder.Property(x=> x.AircraftType).HasConversion<string>();

        }
    }
}