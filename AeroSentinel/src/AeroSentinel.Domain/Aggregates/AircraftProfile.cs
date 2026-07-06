using System;

namespace AeroSentinel.Domain.Entities;

public sealed class AircraftProfile
{
    public string ICAO24 {get; init;} = null!; 
    public string Registration {get;  init;} = null!;
    public AircraftType AircraftType { get;  init;} 
    public AircraftManufacturer? Manufacturer { get;  init;} 
    public AircraftPerformance? AircraftPerformance { get;  init;} 
    public AircraftCredential AircraftCredential {get; private set;} = null!; //for relationship
 
    private AircraftProfile()
    {
        // Parameterless constructor for ORM and serialization
    }

    public AircraftProfile(
        string icao24,
        string registration,
        string aircraftType,
        AircraftPerformance aircraftPerformance
        )
       
    {
        ICAO24 = AircraftProfileValidation.RequireValidICAO24(icao24, nameof(icao24));

        Registration = AircraftProfileValidation.RequirePhilippineRegistration(
                registration, nameof(registration));

        AircraftType = AircraftProfileValidation.RequireValidAircraftTypeCode(
                aircraftType, nameof(aircraftType));

        Manufacturer = AircraftType.GetManufacturer();

        AircraftPerformance = aircraftPerformance;

        aircraftPerformance.ValidatePerformanceEnvelope(Registration);

        
    }
}