namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidAircraftTypeException
    : DomainException
{
    public InvalidAircraftTypeException(
        string? aircraftType,
        string reason)
        : base($"Invalid aircraft type '{aircraftType}'. {reason}")
    {
    }
}