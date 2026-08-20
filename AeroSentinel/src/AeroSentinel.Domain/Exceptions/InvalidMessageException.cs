namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidMessageException
    : DomainException
{
    public InvalidMessageException(
        long? sequence,
        Guid? messageId,
        string reason)
        : base($"Invalid message identifier '{sequence}'. {reason}")
    {
    }
}