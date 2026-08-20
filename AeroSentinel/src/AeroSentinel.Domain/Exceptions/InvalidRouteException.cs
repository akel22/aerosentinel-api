namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidRouteException
    : DomainException
{
    public InvalidRouteException(
        int? sequence,
        string reason)
        : base($"Invalid sequence '{sequence}'. {reason}")
    {
    }
}