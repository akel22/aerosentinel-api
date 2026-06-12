using System;
using AeroSentinel.Domain.Enums;
using AeroSentinel.Domain.Extensions;

namespace AeroSentinel.Domain.Entities;

public sealed class AircraftProfile
{
    public string ICAO24 { get; private set; } = null!; // Unique 24-bit ICAO address, represented as a 6-character hexadecimal string

    // Philippine registration mark RP-C#### 
    public string Registration { get; private set; } = null!;

    // Aircraft classification code, e.g. B738, A320, C172, etc.
    public AircraftType AircraftType { get; private set; } 

    // Derived from aircraft type code, e.g. Boeing, Airbus, Cessna, etc.
    public AircraftManufacturer Manufacturer { get; private set; } 

    // Normal operational characteristics 
    public double CruiseSpeedKnots { get; private set; }

    // Physical performance limits
    public double MaxVelocityKnots { get; private set; }
    public double MaxAltitudeFeet { get; private set; }
    public double MaxClimbRateFeetPerMinute { get; private set; }
    public double MaxDescentRateFeetPerMinute { get; private set; }
    public double MaxTurnRateDegreesPerSecond { get; private set; } 


    private AircraftProfile()
    {
        // Parameterless constructor for ORM and serialization
    }

    public AircraftProfile(
        string icao24,
        string registration,
        string aircraftType,
        double cruiseSpeedKnots,
        double maxVelocityKnots,
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

        CruiseSpeedKnots = AircraftProfileValidation
            .RequirePositive(
                cruiseSpeedKnots,
                nameof(cruiseSpeedKnots));

        MaxVelocityKnots = AircraftProfileValidation
            .RequirePositive(
                maxVelocityKnots,
                nameof(maxVelocityKnots));

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

        ValidatePerformanceEnvelope();
    }

    private void ValidatePerformanceEnvelope()
    {
        if (CruiseSpeedKnots >= MaxVelocityKnots)
        {
            throw new AircraftPerformanceException(
                Registration,
                "Cruise speed must be lower than maximum velocity.");
        }
    }
}