namespace AeroSentinel.Infrastructure.Implementations.Services.Cache;

public sealed class AircraftCredentialCacheService: IAircraftCredentialCacheService
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

    public async Task<byte[]?>GetSharedVerificationAsync(Guid credentialId,CancellationToken cancellationToken)
    {
        var cacheKey = $"credential:{credentialId}";

        if(_cache.TryGetValue(cacheKey, out byte[]? sharedKey))
        {
            _logger.LogInformation("Credential cache hit");

            return sharedKey!.ToArray();
        }

        _logger.LogInformation("Credential cache miss");

        var credential = await _aircraftCredentialRepository.
        GetAircraftCredentialAsync(credentialId, cancellationToken);

        if(credential is null) return null;

        sharedKey = credential.VerificationKey.ToArray();

        var options = new MemoryCacheEntryOptions
            {
                SlidingExpiration = CacheDuration,

                Size = 1
            };

        _cache.Set(cacheKey, sharedKey, options);

        return sharedKey.ToArray();
    }
}