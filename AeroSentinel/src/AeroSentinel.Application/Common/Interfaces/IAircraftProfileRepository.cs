namespace AeroSentinel.Application.Common.Interfaces;

    public interface IAircraftProfileRepository
    {
        Task<IReadOnlyList<AircraftProfile>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<AircraftProfile?> GetByICAO24Async(string icao24, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(AircraftProfile profile, CancellationToken cancellationToken = default);

        Task UpdateAsync(AircraftProfile profile, CancellationToken cancellationToken = default);
    
        
    
    }
