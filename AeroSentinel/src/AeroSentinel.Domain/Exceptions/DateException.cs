namespace AeroSentinel.Domain.Exceptions;
public sealed class DateException
    : DomainException
{
    public DateException(
        DateTime? date,
        string reason)
        : base($"Invalid schedule for '{date}'. {reason}")
    {
    }
}