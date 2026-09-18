using Microsoft.AspNetCore.DataProtection;

namespace AeroSentinel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            MongoDbMapping.ConfigureMappings();

             services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Database=testdb2;Username=postgres;Password=ezekiel-admin22",
                x => x.MigrationsAssembly("AeroSentinel.Infrastructure")));
                            
                var mongoClient = new MongoClient("mongodb://localhost:27017");

                var mongoDatabase = mongoClient.GetDatabase("AeroSentinelDbTest2");

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
