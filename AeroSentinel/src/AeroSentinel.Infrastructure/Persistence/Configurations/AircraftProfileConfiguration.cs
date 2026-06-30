namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class AircraftProfileConfiguration : IEntityTypeConfiguration<AircraftProfile>
    {
        public void Configure(EntityTypeBuilder<AircraftProfile> builder)
        {
           builder.HasKey(x => x.ICAO24);

           builder.HasOne(x => x.AircraftCredential).WithOne()
                  .HasForeignKey<AircraftCredential>(x => x.CredentialId);

           builder.OwnsOne(x => x.AircraftPerformance, _ =>
            {
                _.Property(x => x.CruiseSpeedKnots);
                _.Property(x => x.MaxVelocityKnots);
                _.Property(x => x.MaxAltitudeFeet);
                _.Property(x => x.MaxAltitudeFeet);
                _.Property(x => x.MaxClimbRateFeetPerMinute);
                _.Property(x => x.MaxDescentRateFeetPerMinute);
                _.Property(x => x.MaxTurnRateDegreesPerSecond);

            });

            builder.Property(x=> x.AircraftType).HasConversion<string>();

        }
    }
}