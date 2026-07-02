public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(
            typeof(DependencyInjection).Assembly));//Scans the whole project dll for handlers and registers

        
        return services;    
    }
}