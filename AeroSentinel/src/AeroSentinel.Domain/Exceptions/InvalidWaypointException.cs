namespace AeroSentinel.Domain.Exceptions;
public sealed class InvalidWaypointException
    : DomainException
{
    public InvalidWaypointException(
        string? waypoint,
        string reason)
        : base($"Invalid waypoint '{waypoint}'. {reason}")
    {
    }
}