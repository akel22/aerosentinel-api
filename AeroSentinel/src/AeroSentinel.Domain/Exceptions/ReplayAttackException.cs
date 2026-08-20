namespace AeroSentinel.Domain.Exceptions;
public sealed class ReplayAttackException
    : DomainException
{
    public ReplayAttackException(
        long? sequenceNumber,
        string reason)
        : base($"Invalid sequence for this message.'{sequenceNumber}'. {reason}")
    {
    }
}