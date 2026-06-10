namespace AeroSentinel.Domain.Exceptions;
public sealed class AircraftConditionException
    : DomainException
{
    public AircraftConditionException(
        double? value,
        string reason)
        : base($"Invalid condition data for aircraft '{value}'. {reason}")
    {
    }
}