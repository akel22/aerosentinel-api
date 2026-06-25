using AeroSentinel.Application.Common.Interfaces;
using AeroSentinel.Infrastructure.Persistence;

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

            return services;
        }

        public static IServiceCollection AddTelemetryRepository(this IServiceCollection services)
        {
            services.AddScoped<IAircraftTelemetryRepository, AircraftTelemetryRepository>();

            return services;

        }

    }
}