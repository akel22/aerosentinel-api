namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidAircraftRegistrationException
    : DomainException
{
    public InvalidAircraftRegistrationException(
        string? registration,
        string reason)
        : base($"Invalid aircraft registration '{registration}'. {reason}")
    {
    }
}