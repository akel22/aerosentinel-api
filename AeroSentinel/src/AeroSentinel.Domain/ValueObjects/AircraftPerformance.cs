namespace AeroSentinel.Domain.Entities;
public readonly record struct AircraftPerformance{

    public double CruiseSpeedKnots {get;}
    public double MaxVelocityKnots  {get; }

    public AircraftPerformance(double cruiseSpeedKnots, double maxVelocityKnots ){
    
        CruiseSpeedKnots = cruiseSpeedKnots;
        MaxVelocityKnots = maxVelocityKnots;    
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