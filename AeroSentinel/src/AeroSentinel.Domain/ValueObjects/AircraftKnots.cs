namespace AeroSentinel.Domain.Entities;
public class AircraftKnotsValueObject{

    public double CruiseSpeedKnots {get; private set;}
    public double MaxVelocityKnots  {get; private set;}

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