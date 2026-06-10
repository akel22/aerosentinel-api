using System;

namespace AeroSentinel.Domain.Entities;

public static class FlightTelemetryValidation
{
    public static string RequireValidMessageId(string messageId, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(messageId))
            throw new InvalidMessageIdentifierException(messageId, $"{propertyName} cannot be empty.");
            
        return messageId.Trim();
    }

    public static double RequireValidLatitude(double latitude, string propertyName)
    {
        if (latitude < -90.0 || latitude > 90.0)
                    throw new InvalidCoordinateSystemException(latitude, 
                    $"{propertyName} must be between -90 and 90 degrees.");

        return latitude;
    }

    public static double RequireValidLongitude(double longitude, string propertyName)
    {
        if (longitude < -180.0 || longitude > 180.0)
            throw new InvalidCoordinateSystemException(longitude,
             $"{propertyName} must be between -180 and 180 degrees.");

        return longitude;
    }

    public static double RequireValidTrackDegrees(double trackDegrees, string propertyName)
    {
        if (trackDegrees < 0.0 || trackDegrees > 360.0)
            throw new InvalidCoordinateSystemException(trackDegrees, 
            $"{propertyName} must be between 0 and 360 degrees.");

        return trackDegrees;
    }
    
    public static DateTime RequireValidTimestamp(DateTime timestampUtc, string propertyName)
    {
        if (timestampUtc > DateTime.UtcNow.AddMinutes(5) || timestampUtc < DateTime.UtcNow.AddHours(-24))
            throw new InvalidTimestampException(timestampUtc, $"{propertyName} must be within a reasonable time range.");
        return timestampUtc;
    }

    public static double RequireValidAltitudeFeet(double altitudeFeet, string propertyName)
    {
        if (altitudeFeet < -1000 || altitudeFeet > 60000)
            throw new AircraftConditionException(altitudeFeet, $"{propertyName} must be between -1000 and 60000 feet.");
        
        return altitudeFeet;
    }

    public static double RequireValidGroundSpeedKnots(double groundSpeedKnots, string propertyName)
    {
        if (groundSpeedKnots < 0 || groundSpeedKnots > 1000)
            throw new AircraftConditionException(groundSpeedKnots, $"{propertyName} must be between 0 and 1000 knots.");
        
        return groundSpeedKnots;
    }

    public static double RequireValidVerticalRateFeetPerMinute(double verticalRateFeetPerMinute, string propertyName)
    {
        if (verticalRateFeetPerMinute < -10000 || verticalRateFeetPerMinute > 10000)
            throw new AircraftConditionException(verticalRateFeetPerMinute, $"{propertyName} must be between -10000 and 10000 feet per minute.");
        
        return verticalRateFeetPerMinute;
    }

    public static long RequireValidSequenceNumber(long sequenceNumber, string propertyName)
    {
        if (sequenceNumber < 0)
            throw new InvalidMessageIdentifierException(null, $"{propertyName} must be a non-negative integer.");

        return sequenceNumber;
    }

    public static string RequireValidSignature(string signature, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(signature))
            throw new NullReferenceException($"{propertyName} cannot be null or empty.");

        signature = signature.Trim();

        return signature;
    }

}