namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidAircraftManufacturerException
    : DomainException
{
    public InvalidAircraftManufacturerException(
        string? manufacturer,
        string reason)
        : base($"Invalid aircraft manufacturer '{manufacturer}'. {reason}")
    {
    }
}