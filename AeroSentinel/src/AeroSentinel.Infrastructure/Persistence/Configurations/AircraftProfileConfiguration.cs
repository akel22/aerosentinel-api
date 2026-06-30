namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class AircraftProfileConfiguration : IEntityTypeConfiguration<AircraftProfile>
    {
        public void Configure(EntityTypeBuilder<AircraftProfile> builder)
        {
           builder.HasKey(x => x.ICAO24);

           builder.HasOne(x => x.AircraftCredential).WithOne()
                  .HasForeignKey<AircraftProfile>(x => x.CredentialId);

           builder.OwnsOne(x => x.AircraftPerformance, _ =>
            {
                _.Property(x => x.CruiseSpeedKnots);
                _.Property(x => x.MaxVelocityKnots);
                _.Property(x => x.MaxAltitudeFeet);
                _.Property(x => x.MaxClimbRateFeetPerMinute);
                _.Property(x => x.MaxDescentRateFeetPerMinute);
                _.Property(x => x.MaxTurnRateDegreesPerSecond);

            });

            builder.Property(x=> x.AircraftType).HasConversion<string>();

            builder.HasData(
                new AircraftProfile(
                    "A1B2C3",
                    "RP-C1234",
                    "A320",
                        new AircraftPerformance(
                            20,
                            20,
                            50,
                            20,
                            50,
                            50
                        ),
                    Guid.Parse("9b5e8c6d-7d59-4c7b-bcb3-1d79e7c3e5d8")
                )
            );


        }
    }
}