namespace AeroSentinel.Infrastructure.Implementations.Services.Cache;

public sealed class AircraftCredentialCacheService: IAircraftCredentialCacheService
{
    private readonly IMemoryCache _cache;

    private readonly IAircraftCredentialRepository _repository;

    private readonly ILogger<AircraftCredentialCacheService> _logger;

    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

    public AircraftCredentialCacheService(
        IMemoryCache cache,
        IAircraftCredentialRepository repository,
        ILogger<AircraftCredentialCacheService> logger)
    {
        _cache = cache;
        _repository = repository;
        _logger = logger;
    }

    public async Task<AircraftCredential?>GetCredentialAsync(Guid credentialId,CancellationToken cancellationToken)
    {
        var cacheKey = $"credential:{credentialId}";

        if(_cache.TryGetValue(cacheKey, out AircraftCredential?credential))
        {
            _logger.LogInformation("Credential cache hit");

            return credential;
        }


        _logger.LogInformation("Credential cache miss");

        credential = await _repository.GetByIdAsync(credentialId,cancellationToken);

        if(credential is null) return null;
  

        var options = new MemoryCacheEntryOptions
            {
                SlidingExpiration = CacheDuration,

                Size = 1
            };

        _cache.Set(cacheKey, credential, options);

        return credential;
    }
}