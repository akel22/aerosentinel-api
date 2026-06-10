namespace AeroSentinel.Domain.Exceptions;

public sealed class AircraftPerformanceException
    : DomainException
{
    public AircraftPerformanceException(
        string? registration,
        string reason)
        : base($"Invalid performance data for aircraft '{registration}'. {reason}")
    {
    }
}