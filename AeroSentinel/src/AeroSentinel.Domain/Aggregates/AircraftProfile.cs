using System;

namespace AeroSentinel.Domain.Entities;

public sealed class AircraftProfile
{
    public string ICAO24 {get; init;} = null!; // Unique 24-bit ICAO address, represented as a 6-character hexadecimal string

    // Philippine registration mark RP-C#### 
    public string Registration {get;  init;} = null!;

    // Aircraft classification code, e.g. B738, A320, C172, etc.
    public AircraftType AircraftType { get;  init;} 

    // Derived from aircraft type code, e.g. Boeing, Airbus, Cessna, etc.
    public AircraftManufacturer Manufacturer { get;  init;}  

    // Normal operational characteristics 
    public AircraftKnotsValueObject AircraftKnotsValueObject { get;  init;} 
    public double MaxAltitudeFeet { get;  init;}
    public double MaxClimbRateFeetPerMinute { get;  init;}
    public double MaxDescentRateFeetPerMinute { get;  init;}
    public double MaxTurnRateDegreesPerSecond { get;  init;} 


    private AircraftProfile()
    {
        // Parameterless constructor for ORM and serialization
    }

    public AircraftProfile(
        string icao24,
        string registration,
        string aircraftType,
        AircraftKnotsValueObject aircraftKnotsValueObject,
        double maxAltitudeFeet,
        double maxClimbRateFeetPerMinute,
        double maxDescentRateFeetPerMinute,
        double maxTurnRateDegreesPerSecond)
    {
        ICAO24 = AircraftProfileValidation
            .RequireValidICAO24(icao24, nameof(icao24));

        Registration = AircraftProfileValidation
            .RequirePhilippineRegistration(
                registration,
                nameof(registration));

        AircraftType = AircraftProfileValidation
            .RequireValidAircraftTypeCode(
                aircraftType,
                nameof(aircraftType));

        Manufacturer = AircraftType.GetManufacturer();

        AircraftKnotsValueObject = aircraftKnotsValueObject;

        MaxAltitudeFeet = AircraftProfileValidation
            .RequirePositive(
                maxAltitudeFeet,
                nameof(maxAltitudeFeet));

        MaxClimbRateFeetPerMinute = AircraftProfileValidation
            .RequirePositive(
                maxClimbRateFeetPerMinute,
                nameof(maxClimbRateFeetPerMinute));

        MaxDescentRateFeetPerMinute = AircraftProfileValidation
            .RequirePositive(
                maxDescentRateFeetPerMinute,
                nameof(maxDescentRateFeetPerMinute));

        MaxTurnRateDegreesPerSecond = AircraftProfileValidation
            .RequirePositive(
                maxTurnRateDegreesPerSecond,
                nameof(maxTurnRateDegreesPerSecond));

        AircraftKnotsValueObject.ValidatePerformanceEnvelope(Registration);
    }

}