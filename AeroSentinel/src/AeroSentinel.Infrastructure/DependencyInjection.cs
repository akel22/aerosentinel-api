namespace AeroSentinel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Database=testdb2;Username=postgres;Password=ezekiel-admin22",
                x => x.MigrationsAssembly("AeroSentinel.Infrastructure")));
              

            services.AddScoped<IAircraftTelemetryRepository, AircraftTelemetryRepository>();
             services.AddScoped<IAircraftProfileRepository, AircraftProfileRepository>();
             services.AddScoped<IAircraftCredentialRepository, AircraftCredentialRepository>();
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
                        
        
        return services;

        }

    }
}
