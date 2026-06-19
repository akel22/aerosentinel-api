namespace AeroSentinel.Domain.Exceptions;
public sealed class NullException
    : DomainException
{
    public NullException(
        string? reference,
        string reason)
        : base($"Property {reference} cannot be null. {reason}")
    {
    }
}