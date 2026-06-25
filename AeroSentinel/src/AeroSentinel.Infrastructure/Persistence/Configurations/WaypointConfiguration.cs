public sealed class WaypointConfiguration : IEntityTypeConfiguration<Waypoint>
{
    public void Configure(EntityTypeBuilder<Waypoint>builder)
    {
        builder.HasKey(x =>x.WaypointId);

        builder.Property(x =>x.Latitude).IsRequired();

        builder.Property(x =>x.Longitude).IsRequired();
    }
}