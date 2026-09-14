namespace AeroSentinel.Infrastructure.MongoDB;

public static class MongoDbMapping
{
    public static void ConfigureMappings()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(FlightTelemetry)))
        {
            return;
        }

        var pack = new ConventionPack
        {
            new CamelCaseElementNameConvention()
        };

        ConventionRegistry.Register(
            "CamelCase",
            pack,
            _ => true
        );

        // 1. Map Value Objects first
        ConfigureValueObjects();

        // 2. Map FlightTelemetry Aggregate Root
        BsonClassMap.RegisterClassMap<FlightTelemetry>(map =>
        {
            map.AutoMap();

            // Primary Key: Map MessageId to MongoDB '_id' as Standard Guid Representation
            map.MapIdProperty(x => x.MessageId)
               .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));

            // Element Mappings with lowerCamelCase BSON names
            map.MapProperty(x => x.ICAO24).SetElementName("icao24");
            map.MapProperty(x => x.FlightPlanId)
               .SetElementName("flightPlanId")
               .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            
            map.MapProperty(x => x.Callsign).SetElementName("callsign");
            map.MapProperty(x => x.Squawk).SetElementName("squawk");
            map.MapProperty(x => x.TimestampUtc).SetElementName("timestampUtc");

            // Value Objects Embedded Documents
            map.MapProperty(x => x.SpatialState).SetElementName("spatialState");
            map.MapProperty(x => x.FlightIntent).SetElementName("flightIntent");

            // Security & Telemetry Tracking
            map.MapProperty(x => x.SequenceNumber).SetElementName("sequenceNumber");
            map.MapProperty(x => x.Signature).SetElementName("signature");

            // Status Enum mapped as String instead of Integer for MongoDB readability
            map.MapProperty(x => x.Status)
               .SetElementName("status")
               .SetSerializer(new EnumSerializer<TelemetryStatus>(BsonType.String));

            //map.MapProperty(x => x.FailureReason).SetElementName("failureReason");

            // Ignore navigation properties (relational concepts shouldn't embed whole roots)
            map.UnmapProperty(x => x.FlightPlan);
        });
    }

   private static void ConfigureValueObjects()
{
    if (!BsonClassMap.IsClassMapRegistered(typeof(SpatialState)))
    {
        BsonClassMap.RegisterClassMap<SpatialState>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
        });
    }

    if (!BsonClassMap.IsClassMapRegistered(typeof(FlightIntent)))
    {
        BsonClassMap.RegisterClassMap<FlightIntent>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
        });
    }
}
}