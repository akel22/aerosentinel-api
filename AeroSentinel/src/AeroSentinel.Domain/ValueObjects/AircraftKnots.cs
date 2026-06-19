namespace AeroSentinel.Domain.Entities;
public readonly record struct AircraftKnotsValueObject{

    public double CruiseSpeedKnots {get;}
    public double MaxVelocityKnots  {get; }

    public AircraftKnotsValueObject(double cruiseSpeedKnots, double maxVelocityKnots ){
    
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