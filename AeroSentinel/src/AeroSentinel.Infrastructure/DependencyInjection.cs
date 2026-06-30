

namespace AeroSentinel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=Persistence/testingDB.db"));

             services.AddScoped<IAircraftTelemetryRepository, AircraftTelemetryRepository>();
             services.AddScoped<IAircraftProfileRepository, AircraftProfileRepository>();
             services.AddScoped<ICryptographyService, CryptographyService>();
             services.AddScoped<IReplayProtectionService, ReplayProtectionService>();
             services.AddScoped<WaypointsCSVService>();

            
            return services;

        }

    }
}