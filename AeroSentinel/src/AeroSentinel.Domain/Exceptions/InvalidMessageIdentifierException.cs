namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidMessageIdentifierException
    : DomainException
{
    public InvalidMessageIdentifierException(
        string? messageId,
        string reason)
        : base($"Invalid message identifier '{messageId}'. {reason}")
    {
    }
}