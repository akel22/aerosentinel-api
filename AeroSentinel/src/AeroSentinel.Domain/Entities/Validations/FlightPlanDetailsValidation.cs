using System;

namespace AeroSentinel.Domain.Entities.Validations;

public static class FlightPlanDetailsValidation
{
    public static string RequireValidCallsign(string callsign, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(callsign))
        {
             throw new InvalidCallsignException(callsign, $"{propertyName} is required.");
        }
        if (callsign.Length <= 0)
        {
             throw new InvalidCallsignException(callsign, $"{propertyName} must be greater than 0 characters.");
        }
        callsign = callsign.Trim().ToUpperInvariant();

        return callsign;
           
    }

    public static string RequireValidAirportCode(string airportCode, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(airportCode))
        {
            throw new InvalidICAOCodeException(airportCode, $"{propertyName} is required.");
        }

        airportCode = airportCode.Trim().ToUpperInvariant();

        if (airportCode.Length != 4)
        {
            throw new InvalidICAOCodeException(airportCode, $"{propertyName} must be a valid ICAO airport code.");
        }

        return airportCode;
    }

    public static string RequireValidWaypoint(string waypoint, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(waypoint))
        {
            throw new InvalidWaypointException(waypoint, $"{propertyName} is required. Cannot be null or empty.");
        }
        if(waypoint.Length <= 0)
        {
            throw new InvalidWaypointException(waypoint, $"{propertyName} must be greater than 0 characters.");
        }

        waypoint = waypoint.Trim().ToUpperInvariant();

        return waypoint;
           
    }

    public static DateTime RequireValidDepartureTime( DateTime departureTimeUtc, string propertyName)
    {
        if (departureTimeUtc == default)
        {
            throw new DateException(departureTimeUtc, $"{propertyName} is not a valid date.");
        }

        return departureTimeUtc;
    }

    public static DateTime RequireValidArrivalTime(DateTime arrivalTimeUtc, DateTime departureTimeUtc,
        string propertyName)
    {
        if (arrivalTimeUtc == default)
        {
           throw new DateException(arrivalTimeUtc, $"{propertyName} is not a valid date.");
        }

        if (arrivalTimeUtc <= departureTimeUtc)
        {
            throw new DateException(arrivalTimeUtc, $"{propertyName} must occur after departure time.");
        }

        return arrivalTimeUtc;
    }
}