namespace AeroSentinel.Domain.Exceptions;
public sealed class ValueObjectException
    : DomainException
{
    public ValueObjectException(
        Object? valueObject,
        string reason)
        : base($"Invalid value object '{valueObject}'. {reason}")
    {
    }
}