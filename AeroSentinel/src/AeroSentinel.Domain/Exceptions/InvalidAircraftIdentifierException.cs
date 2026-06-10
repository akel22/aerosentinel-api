namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidAircraftIdentifierException
    : DomainException
{
    public InvalidAircraftIdentifierException(
        string? aircraftId,
        string reason)
        : base($"Invalid aircraft identifier '{aircraftId}'. {reason}")
    {
    }
}