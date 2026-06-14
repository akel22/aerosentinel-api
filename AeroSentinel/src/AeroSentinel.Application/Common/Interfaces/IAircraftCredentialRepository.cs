namespace AeroSentinel.Application.Common.Interfaces;
public interface IAircraftCredentialRepository
{
    Task<AircraftCredential?> GetByICAO24Async(string icao24, CancellationToken cancellationToken = default);

}