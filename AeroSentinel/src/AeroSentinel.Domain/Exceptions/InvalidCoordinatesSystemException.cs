using AeroSentinel.Domain.ValueObjects;

namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidCoordinatesSystemException
    : DomainException
{
    public InvalidCoordinatesSystemException(
        double? coordinates,
        string reason)
        : base($"Coordinate out of bounds '{coordinates}'. {reason}")
    {
    }
}