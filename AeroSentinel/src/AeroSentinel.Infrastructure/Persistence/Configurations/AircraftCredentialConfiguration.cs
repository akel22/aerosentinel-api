namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class AircraftCredentialConfiguration : IEntityTypeConfiguration<AircraftCredential>
    {
        public void Configure(EntityTypeBuilder<AircraftCredential> builder)
        {
           builder.HasKey(x => x.CredentialId);
        
           builder.Property(x => x.Status).HasConversion<string>();

           builder.Property<byte[]>("VerificationKey").IsRequired();

           builder.HasOne(x => x.AircraftProfile)
                      .WithOne(x => x.AircraftCredential)
                      .HasForeignKey<AircraftCredential>(x => x.ICAO24);
                 
        }
    }
}