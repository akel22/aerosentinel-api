public interface IAircraftProfileRepository
{
    Task<AircraftProfile?> GetByICAO24Async(string icao24, CancellationToken cancellationToken = default);
 
}