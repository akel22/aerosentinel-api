namespace AeroSentinel.Domain.Entities;

public sealed class AircraftPerformance
{
    public double CruiseSpeedKnots { get; init; }
    public double MaxVelocityKnots { get; init; }
    public double MaxAltitudeFeet { get; init; }
    public double MaxClimbRateFeetPerMinute { get; init; }
    public double MaxDescentRateFeetPerMinute { get; init; }
    public double MaxTurnRateDegreesPerSecond { get; init; }

    private AircraftPerformance()
    {
        
    }

    public AircraftPerformance(
        double cruiseSpeedKnots,
        double maxVelocityKnots,
        double maxAltitudeFeet,
        double maxClimbRateFeetPerMinute,
        double maxDescentRateFeetPerMinute,
        double maxTurnRateDegreesPerSecond)
    {

        CruiseSpeedKnots = AircraftProfileValidation.RequirePositive(
            cruiseSpeedKnots, nameof(cruiseSpeedKnots));

        MaxVelocityKnots = AircraftProfileValidation.RequirePositive(
            maxVelocityKnots, nameof(maxVelocityKnots));

        MaxAltitudeFeet = AircraftProfileValidation.RequirePositive(
               maxAltitudeFeet, nameof(maxAltitudeFeet));

        MaxClimbRateFeetPerMinute = AircraftProfileValidation.RequirePositive(
            maxClimbRateFeetPerMinute, nameof(maxClimbRateFeetPerMinute));

        MaxDescentRateFeetPerMinute = AircraftProfileValidation.RequirePositive(
            maxDescentRateFeetPerMinute, nameof(maxDescentRateFeetPerMinute));

        MaxTurnRateDegreesPerSecond = AircraftProfileValidation.RequirePositive(
                maxTurnRateDegreesPerSecond, nameof(maxTurnRateDegreesPerSecond));
    }


    public void ValidatePerformanceEnvelope(string registration)
    {

        if (CruiseSpeedKnots >= MaxVelocityKnots)
        {
            throw new AircraftPerformanceException(
                registration,
                "Cruise speed must be lower than maximum velocity.");
        }
    }

}