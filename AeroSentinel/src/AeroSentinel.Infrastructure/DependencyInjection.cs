using AeroSentinel.Application.Services.Interfaces;

namespace AeroSentinel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Default")));

             services.AddScoped<IAircraftTelemetryRepository, AircraftTelemetryRepository>();
             services.AddScoped<ICryptographyService, CryptographyService>();
             services.AddScoped<IReplayProtectionService, ReplayProtectionService>();

            
            return services;

        }

    }
}