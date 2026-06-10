namespace AeroSentinel.Domain.Exceptions;
public sealed class InvalidTimestampException
    : DomainException
{
    public InvalidTimestampException(
        DateTime timestamp,
        string reason)
        : base($"Invalid timestamp '{timestamp}'. {reason}")
    {
    }
}