namespace AeroSentinel.Domain.Exceptions;

public sealed class GuidException
    : DomainException
{
    public GuidException(
        Guid? guid,
        string reason)
        : base($"Invalid Guid: ['{guid}']. {reason}")
    {
    }
}