namespace AeroSentinel.Domain.Exceptions;
public sealed class AircraftCredentialException
    : DomainException
{
    public AircraftCredentialException(
        string? ICAO24,
        Guid? credentialId,
        string reason)
        : base($"Invalid credentials for aircraft: '{ICAO24}' [{credentialId} is Invalid]. {reason}")
    {
    }
}