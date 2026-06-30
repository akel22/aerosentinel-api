namespace AeroSentinel.Infrastructure.Persistence.Configurations
{
    public sealed class AircraftCredentialConfiguration : IEntityTypeConfiguration<AircraftCredential>
    {
        public void Configure(EntityTypeBuilder<AircraftCredential> builder)
        {
           builder.HasKey(x => x.CredentialId);
        
           builder.Property(x => x.Status).HasConversion<string>();

           builder.Property<byte[]>("VerificationKey").IsRequired();

           builder.HasData(
             new AircraftCredential(
                Guid.Parse("9b5e8c6d-7d59-4c7b-bcb3-1d79e7c3e5d8"),
                "A1B2C3",
                [
                    0x2F, 0x81, 0xD7, 0x4C,
                    0x9A, 0x15, 0x63, 0xBE,
                    0xF0, 0x28, 0x7D, 0x91,
                    0xAA, 0x55, 0x10, 0xEF,
                    0xC3, 0x6A, 0xB4, 0x39,
                    0x5E, 0x77, 0x8D, 0x42,
                    0x19, 0xE0, 0x34, 0xCB,
                    0x61, 0x94, 0xFA, 0x08
                    ])
            );
                 
        }
    }
}