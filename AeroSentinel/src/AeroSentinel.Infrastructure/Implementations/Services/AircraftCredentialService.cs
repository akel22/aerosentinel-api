namespace AeroSentinel.Infrastructure.Implementations.Services;

public sealed class AircraftCredentialCacheService : IAircraftCredentialCacheService
{
    private readonly IMemoryCache _cache;

    private readonly IAircraftCredentialRepository _aircraftCredentialRepository;

    private readonly ILogger<AircraftCredentialCacheService> _logger;

    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(1);

    public AircraftCredentialCacheService(
        IMemoryCache cache,
        IAircraftCredentialRepository aircraftCredentialRepository,
        ILogger<AircraftCredentialCacheService> logger)
    {
        _cache = cache;
        _aircraftCredentialRepository = aircraftCredentialRepository;
        _logger = logger;
    }

    public async Task<byte[]?> GetSharedVerificationAsync(string ICAO24, CancellationToken cancellationToken)
    {
        var cacheKey = $"credential:{ICAO24.ToUpperInvariant().Trim()}";

        if (_cache.TryGetValue(cacheKey, out byte[]? sharedKey))
        {
            _logger.LogInformation("Credential cache hit for {ICAO24}", ICAO24);
            return [.. sharedKey!];
        }

        _logger.LogInformation("Credential cache miss");

        var credential = await _aircraftCredentialRepository
            .GetAircraftCredentialAsync(ICAO24, cancellationToken);

        if (credential is null)
        {
            return null;
        }

        sharedKey = [.. credential.VerificationKey];

        var options = new MemoryCacheEntryOptions
        {
            SlidingExpiration = CacheDuration,

            Size = 1
        };

        _cache.Set(cacheKey, sharedKey, options);

        return [.. sharedKey];
    }
}
