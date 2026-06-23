using System;

namespace AeroSentinel.Domain.Entities;

public sealed class AircraftProfile
{
    public string ICAO24 {get; init;} = null!; 

    public string Registration {get;  init;} = null!;

    public AircraftType AircraftType { get;  init;} 

    public AircraftManufacturer Manufacturer { get;  init;}  

    public AircraftPerformance AircraftPerformance { get;  init;} 

    public Guid CredentialId {get; init;} 
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
        AircraftPerformance aircraftPerformance,
        Guid credentialId,
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

        aircraftPerformance.ValidatePerformanceEnvelope(Registration);

        AircraftPerformance = aircraftPerformance;

        CredentialId = credentialId; //VALIDATION PA

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

    }

}