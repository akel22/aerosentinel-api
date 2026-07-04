namespace AeroSentinel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=../AeroSentinel.Infrastructure/Persistence/Testing.db",
                x => x.MigrationsAssembly("AeroSentinel.Infrastructure")));
              

            services.AddScoped<IAircraftTelemetryRepository, AircraftTelemetryRepository>();
             services.AddScoped<IAircraftProfileRepository, AircraftProfileRepository>();
             services.AddScoped<IAircraftCredentialRepository, AircraftCredentialRepository>();
             services.AddScoped<ICryptographyService, CryptographyService>();
             services.AddSingleton<IReplayProtectionService, ReplayProtectionService>();
             services.AddScoped<IAircraftCredentialCacheService, AircraftCredentialCacheService>();
             services.AddSingleton<IMemoryCache, MemoryCache>();
             services.AddScoped<WaypointsCSVService>();
            
            return services;

        }

    }
}