namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidCoordinateSystemException
    : DomainException
{
    public InvalidCoordinateSystemException(
        double? point,
        string reason)
        : base($"Coordinate out of bounds '{point}'. {reason}")
    {
    }
}