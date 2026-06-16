using System;
using AeroSentinel.Domain.Exceptions;
using AeroSentinel.Domain.ValueObjects;

namespace AeroSentinel.Domain.Entities;

public static class FlightTelemetryValidation
{
    // Philippine Flight Information Region (FIR) Geofencing Constants
    private const double PhMinLatitude = 4.0;
    private const double PhMaxLatitude = 22.0;
    private const double PhMinLongitude = 116.0;
    private const double PhMaxLongitude = 127.0;

    // Commercial Passenger Fleet Capability Constants (A320/A321/B737/ATR)
    private const double MaxCommercialAltitudeFeet = 42000.0;
    private const double MaxCommercialGroundSpeedKnots = 600.0;
    private const double MaxCommercialVerticalRateFpm = 5000.0; // Dynamic structural cap

    public static string RequireValidMessageId(string messageId, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(messageId))
            throw new InvalidMessageIdentifierException(messageId, $"{propertyName} cannot be empty.");
            
        return messageId.Trim();
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
    
    public static DateTime RequireValidTimestamp(DateTime timestampUtc, string propertyName)
    {
        // Guard against latency anomalies or replay streams trying to feed stale telemetry
        if (timestampUtc > DateTime.UtcNow.AddMinutes(5) || timestampUtc < DateTime.UtcNow.AddHours(-24))
            throw new InvalidTimestampException(timestampUtc, $"{propertyName} falls outside the permissible real-time synchronization window.");
        
        return timestampUtc;
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
            throw new InvalidMessageIdentifierException(null, $"{propertyName} must be an incremental non-negative sequence counter.");

        return sequenceNumber;
    }

    public static string RequireValidSignature(string signature, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(signature))
            throw new ArgumentException("Cryptographic transmission payload requires an authentication signature block.", propertyName);

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

    public static double RequireValidBaroAltitude(double altitudeFeet, string paramName)
    {
        if (altitudeFeet is < -2000.0 or > 85000.0)
            throw new ArgumentOutOfRangeException(paramName, "Barometric altitude must be between -2,000 and 85,000 feet.");

        return altitudeFeet;
    }

    public static double RequireValidSelectedAutoPilotAltitudeFeet(double altitudeFeet, string paramName)
    {
        if (altitudeFeet is < 0.0 or > 45000.0)
        {
            throw new ArgumentOutOfRangeException(paramName, altitudeFeet,
                "Selected altitude violates CAAP commercial flight rules. Must be between 0 and 45,000 feet.");
        }
        return altitudeFeet;
    }

    public static double RequireValidVerticalRate(double verticalRateFpm, string paramName)
    {
        if (verticalRateFpm is < -12000.0 or > 60000.0) // Kept max limit structured or bound directly to +6000.0
        {
            // Tailoring strictly to commercial envelope
            if (verticalRateFpm < -12000.0 || verticalRateFpm > 6000.0)
            {
                throw new ArgumentOutOfRangeException(paramName, verticalRateFpm,
                    "Vertical rate exceeds commercial airline capabilities (-12k FPM emergency dive to +6k FPM max climb).");
            }
        }
        return verticalRateFpm;
    }

    public static double RequireValidIndicatedAirspeed(double airspeedKnots, string paramName)
    {
        if (airspeedKnots is < 0.0 or > 450.0)
        {
            throw new ArgumentOutOfRangeException(paramName, airspeedKnots,
                "Indicated airspeed exceeds maximum commercial airframe structural boundaries (Max 450 knots IAS).");
        }
        return airspeedKnots;
    }

    public static double RequireValidMagneticHeading(double headingDegrees, string paramName)
    {
        if (headingDegrees is < 0.0 or > 360.0)
        {
            throw new ArgumentOutOfRangeException(paramName, headingDegrees,
                "Magnetic heading must be a valid commercial track coordinate between 0.0 and 360.0 degrees.");
        }
        return headingDegrees;
    }
    public static double RequireValidRollAngle(double rollDegrees, string paramName)
    {
        if (rollDegrees is < -70.0 or > 70.0)
        {
            throw new ArgumentOutOfRangeException(paramName, rollDegrees,
                "Roll angle exceeds commercial fly-by-wire structural thresholds (Max 70-degree structural bank limit).");
        }
        return rollDegrees;
    }

     public static AircraftGeoCoordinates RequireValidCoord(AircraftGeoCoordinates aircraftGeoCoordinates,
    string propertyName)
    {
        if(aircraftGeoCoordinates is null)
        {
            throw new InvalidCoordinatesSystemException(null,"Coordinates cannot be null");
        }

        return aircraftGeoCoordinates;
        
    }

    public static SpatialState RequireValidSpatialState(SpatialState spatialState, string propertyName)
    {
         if(spatialState is null)
        {
            throw new ValueObjectException(spatialState, "Spatial state of the data cannot be null");
        }

        return spatialState;    

    }

     public static FlightIntent RequireValidFlightIntent(FlightIntent flightIntent, string propertyName)
    {
         if(flightIntent is null)
        {
            throw new ValueObjectException(flightIntent, "Flight intent of the data cannot be null");
        }

        return flightIntent;

    }
}


