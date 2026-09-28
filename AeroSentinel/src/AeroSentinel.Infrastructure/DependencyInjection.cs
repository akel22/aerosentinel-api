using Microsoft.AspNetCore.DataProtection;

namespace AeroSentinel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            MongoDbMapping.ConfigureMappings();

            var postgresConnectionString = configuration.GetConnectionString("Postgres")
                ?? throw new InvalidOperationException("Connection string 'Postgres' is not configured.");
            var mongoConnectionString = configuration["Mongo:ConnectionString"]
                ?? throw new InvalidOperationException("Mongo connection string is not configured.");
            var mongoDatabaseName = configuration["Mongo:Database"]
                ?? throw new InvalidOperationException("Mongo database name is not configured.");

             services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(postgresConnectionString,
                x => x.MigrationsAssembly("AeroSentinel.Infrastructure")));
                            
                var mongoClient = new MongoClient(mongoConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);

             services.AddSingleton<IMongoDatabase>(mongoDatabase);
              
             services.AddScoped<IAircraftTelemetryRepository, MongoAircraftTelemetryRepository>();

            //  services.AddScoped<IAircraftTelemetryRepository, AircraftTelemetryRepository>();
             services.AddScoped<IAircraftProfileRepository, AircraftProfileRepository>();
             services.AddScoped<IAircraftCredentialRepository, AircraftCredentialRepository>();
             services.AddScoped<IWaypointRepository, WaypointRepository>();

             services.AddScoped<IFlightPlanRepository, FlightPlanRepository>();
             services.AddScoped<ICryptographyService, CryptographyService>();
             services.AddSingleton<IReplayProtectionService, ReplayProtectionService>();
             services.AddScoped<IAircraftCredentialCacheService, AircraftCredentialCacheService>();
             services.AddMemoryCache(options => options.SizeLimit = 1_024);
             services.AddScoped<WaypointsCSVService>();

            
             services.AddSingleton(_ =>
                Channel.CreateBounded<SendTelemetryCommand>(new BoundedChannelOptions(100)
                {
                    FullMode = BoundedChannelFullMode.Wait,
                    SingleWriter = false,
                    SingleReader = true
                }));

             services.AddSingleton(provider => provider.GetRequiredService<Channel<SendTelemetryCommand>>().Reader);
             services.AddSingleton(provider => provider.GetRequiredService<Channel<SendTelemetryCommand>>().Writer);

             services.AddHostedService<FlightTelemetryConsumerService>();
                        
            services.AddDataProtection()
            .SetApplicationName("AeroSentinel")
            .PersistKeysToFileSystem(new DirectoryInfo(@"./keys"));

            services.AddScoped<ICredentialEncryptionService, CredentialEncryptionService>();
            
        return services;

        }

    }
}
