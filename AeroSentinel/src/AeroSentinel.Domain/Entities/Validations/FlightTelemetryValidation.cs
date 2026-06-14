using System;
using AeroSentinel.Domain.Exceptions;

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
            throw new InvalidCoordinateSystemException(latitude, 
                $"{propertyName} ({latitude}) falls outside the Philippine Flight Information Region (FIR) boundaries ({PhMinLatitude}°N to {PhMaxLatitude}°N).");

        return latitude;
    }

    public static double RequireValidLongitude(double longitude, string propertyName)
    {
        if (longitude < PhMinLongitude || longitude > PhMaxLongitude)
            throw new InvalidCoordinateSystemException(longitude,
                $"{propertyName} ({longitude}) falls outside the Philippine Flight Information Region (FIR) boundaries ({PhMinLongitude}°E to {PhMaxLongitude}°E).");

        return longitude;
    }

    public static double RequireValidTrackDegrees(double trackDegrees, string propertyName)
    {
        if (trackDegrees < 0.0 || trackDegrees > 360.0)
            throw new InvalidCoordinateSystemException(trackDegrees, 
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

    public static double RequireValidAltitudeFeet(double altitudeFeet, string propertyName)
    {
        // -1000 accounts for runways slightly below sea level or sensor calibration variations
        if (altitudeFeet < -1000 || altitudeFeet > MaxCommercialAltitudeFeet)
            throw new AircraftConditionException(altitudeFeet, 
                $"{propertyName} ({altitudeFeet} ft) violates the maximum structural ceiling for domestic commercial airliners ({MaxCommercialAltitudeFeet} ft).");
        
        return altitudeFeet;
    }

    public static double RequireValidGroundSpeedKnots(double groundSpeedKnots, string propertyName)
    {
        if (groundSpeedKnots < 0 || groundSpeedKnots > MaxCommercialGroundSpeedKnots)
            throw new AircraftConditionException(groundSpeedKnots, 
                $"{propertyName} ({groundSpeedKnots} kts) exceeds the maximum sub-sonic cruise boundary for commercial fleets ({MaxCommercialGroundSpeedKnots} kts).");
        
        return groundSpeedKnots;
    }

    public static double RequireValidVerticalRateFeetPerMinute(double verticalRateFeetPerMinute, string propertyName)
    {
        if (verticalRateFeetPerMinute < MaxCommercialVerticalRateFpm || verticalRateFeetPerMinute > MaxCommercialVerticalRateFpm)
            throw new AircraftConditionException(verticalRateFeetPerMinute, 
                $"{propertyName} ({verticalRateFeetPerMinute} ft/min) represents an aerodynamic impossibility for stable passenger service configurations.");
        
        return verticalRateFeetPerMinute;
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
}