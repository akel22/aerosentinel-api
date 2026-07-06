using System;
using AeroSentinel.Domain.Exceptions;
using AeroSentinel.Domain.ValueObjects;

namespace AeroSentinel.Domain.Entities;

public static class FlightTelemetryValidation
{
    // Philippine Flight Information Region (FIR) Geofencing Constants
    private const double PhMinLatitude = 3.5;
    private const double PhMaxLatitude = 21.1;
    private const double PhMinLongitude = 114.0;
    private const double PhMaxLongitude = 132.5;

    // Commercial Passenger Fleet Capability Constants (A320/A321/B737/ATR)
    private const double MaxCommercialAltitudeFeet = 42000.0;
    private const double MaxCommercialGroundSpeedKnots = 600.0;
    private const double MaxCommercialVerticalRateFpm = 5000.0; // Dynamic structural cap

    public static Guid RequireValidMessageId(Guid messageId, string? propertyName)
    {
        if (messageId == Guid.Empty)
            throw new InvalidMessageException(null, messageId, $"{propertyName} cannot be empty.");
            
        return messageId;
    }

    public static double RequireValidLatitude(double latitude, string propertyName)
    {
        if (latitude < PhMinLatitude || latitude > PhMaxLatitude)
            throw new InvalidCoordinatesSystemException(latitude, 
                $"{propertyName} ({latitude}) falls outside the Philippine Flight Information Region (FIR) boundaries ({PhMinLatitude}°N to {PhMaxLatitude}°N).");

        return latitude;
    }

    public static double RequireValidLongitude(double longitude, string propertyName)
    {
        if (longitude < PhMinLongitude || longitude > PhMaxLongitude)
            throw new InvalidCoordinatesSystemException(longitude,
                $"{propertyName} ({longitude}) falls outside the Philippine Flight Information Region (FIR) boundaries ({PhMinLongitude}°E to {PhMaxLongitude}°E).");

        return longitude;
    }

    public static double RequireValidTrackAngleDegrees(double trackDegrees, string propertyName)
    {
        if (trackDegrees < 0.0 || trackDegrees > 360.0)
            throw new InvalidCoordinatesSystemException(trackDegrees, 
                $"{propertyName} must be a valid compass bearing between 0 and 360 degrees.");

        return trackDegrees;
    }
    

    public static double RequireValidGroundSpeedKnots(double groundSpeedKnots, string propertyName)
    {
        if (groundSpeedKnots < 0 || groundSpeedKnots > MaxCommercialGroundSpeedKnots)
            throw new AircraftConditionException(groundSpeedKnots, 
                $"{propertyName} ({groundSpeedKnots} kts) exceeds the maximum sub-sonic cruise boundary for commercial fleets ({MaxCommercialGroundSpeedKnots} kts).");
        
        return groundSpeedKnots;
    }

    public static long RequireValidSequenceNumber(long sequenceNumber, string propertyName)
    {
        if (sequenceNumber < 0)
            throw new InvalidMessageException(null, null, $"{propertyName} must be an incremental non-negative sequence counter.");

        return sequenceNumber;
    }

    public static string RequireValidSignature(string signature, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(signature))
            throw new SignatureException("Cryptographic transmission payload requires an authentication signature block.", propertyName);

        return signature.Trim();
    }


    public static string RequireValidSquawk(string squawk, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(squawk))
            throw new InvalidAircraftIdentifierException("Squawk code cannot be null or empty.", propertyName);

        if (!Regex.IsMatch(squawk, @"^[0-7]{4}$"))
            throw new InvalidAircraftIdentifierException("Squawk code must be exactly 4 digits containing only numbers from 0 to 7 (Octal structure).", propertyName);

        return squawk.Trim();
    }

    public static double RequireValidBaroAltitude(double altitudeFeet, string propertyName)
    {
        if (altitudeFeet is < -2000.0 or > 85000.0)
            throw new ArgumentOutOfRangeException(propertyName, "Barometric altitude must be between -2,000 and 85,000 feet.");

        return altitudeFeet;
    }

    public static double RequireValidSelectedAutoPilotAltitudeFeet(double altitudeFeet, string propertyName)
    {
        if (altitudeFeet is < 0.0 or > 45000.0)
        {
            throw new ArgumentOutOfRangeException(propertyName, altitudeFeet,
                "Selected altitude violates CAAP commercial flight rules. Must be between 0 and 45,000 feet.");
        }
        return altitudeFeet;
    }

    public static double RequireValidVerticalRate(double verticalRateFpm, string propertyName)
    {
        if (verticalRateFpm is < -12000.0 or > 60000.0) // Kept max limit structured or bound directly to +6000.0
        {
            // Tailoring strictly to commercial envelope
            if (verticalRateFpm < -12000.0 || verticalRateFpm > 6000.0)
            {
                throw new ArgumentOutOfRangeException(propertyName, verticalRateFpm,
                    "Vertical rate exceeds commercial airline capabilities (-12k FPM emergency dive to +6k FPM max climb).");
            }
        }
        return verticalRateFpm;
    }

    public static double RequireValidIndicatedAirspeed(double airspeedKnots, string propertyName)
    {
        if (airspeedKnots is < 0.0 or > 450.0)
        {
            throw new ArgumentOutOfRangeException(propertyName, airspeedKnots,
                "Indicated airspeed exceeds maximum commercial airframe structural boundaries (Max 450 knots IAS).");
        }
        return airspeedKnots;
    }

    public static double RequireValidMagneticHeading(double headingDegrees, string propertyName)
    {
        if (headingDegrees is < 0.0 or > 360.0)
        {
            throw new ArgumentOutOfRangeException(propertyName, headingDegrees,
                "Magnetic heading must be a valid commercial track coordinate between 0.0 and 360.0 degrees.");
        }
        return headingDegrees;
    }
    public static double RequireValidRollAngle(double rollDegrees, string propertyName)
    {
        if (rollDegrees is < -70.0 or > 70.0)
        {
            throw new ArgumentOutOfRangeException(propertyName, rollDegrees,
                "Roll angle exceeds commercial fly-by-wire structural thresholds (Max 70-degree structural bank limit).");
        }
        return rollDegrees;
    }

     public static AircraftGeoCoordinates RequireValidCoord(AircraftGeoCoordinates aircraftGeoCoordinates,
    string propertyName)
    {
        if(aircraftGeoCoordinates == default)
        {
            throw new InvalidCoordinatesSystemException(null,"Coordinates must be set properly");
        }

        return aircraftGeoCoordinates;
        
    }

    public static SpatialState RequireValidSpatialState(SpatialState spatialState, string propertyName)
    {
         if(spatialState == default)
        {
            throw new ValueObjectException(spatialState, "Spatial state of the data cannot be null");
        }

        return spatialState;    

    }

     public static FlightIntent RequireValidFlightIntent(FlightIntent flightIntent, string propertyName)
    {
         if(flightIntent == default)
        {
            throw new ValueObjectException(flightIntent, "Flight intent of the data cannot be null");
        }

        return flightIntent;

    }

     public static Guid RequireValidFlightPlanId(Guid guid, string? propertyName){

        if(guid == Guid.Empty)
        {
            throw new GuidException(guid, $"{propertyName} must not be empty");
        }

        return guid;
    }

}


