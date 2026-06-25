using System;

namespace AeroSentinel.Domain.Entities.Validations;

public static class PhilippineFlightPlanDetailsValidation
{

   public static string RequireValidCallsign(string callsign, string propertyName)
{
    if (string.IsNullOrWhiteSpace(callsign))
    {
        throw new InvalidCallsignException(callsign, $"{propertyName} is required.");
    }

    if (callsign.Length > 7)
    {
        throw new InvalidCallsignException(callsign, $"{propertyName} must not exceed 7 characters.");
    }

    callsign = callsign.Trim().ToUpperInvariant();


    if (!Regex.IsMatch(callsign, @"^[A-Z]{3}[0-9]{1,4}$"))
    {
        throw new InvalidCallsignException(
            callsign,
            $"{propertyName} format is invalid.");
    }


    return callsign;
}

   public static string RequireValidAirportCode(string airportCode, string propertyName)
{
    if (string.IsNullOrWhiteSpace(airportCode))
    {
        throw new InvalidICAOCodeException(airportCode, $"{propertyName} is required.");
    }

    airportCode = airportCode.Trim().ToUpperInvariant();

    if (!Regex.IsMatch(
        airportCode,
        @"^RP[A-Z]{2}$"))
    {
        throw new InvalidICAOCodeException(airportCode,
            $"{propertyName} must be a Philippine ICAO airport code.");
    }

    return airportCode;
}


    public static DateTime RequireValidDepartureTime(DateTime departureTimeUtc, string propertyName)
    {
       if (departureTimeUtc == default)
        {
            throw new DateException(departureTimeUtc, $"{propertyName} is not a valid date.");
        }

        if (departureTimeUtc.Kind != DateTimeKind.Utc)
        {
            throw new DateException(departureTimeUtc, $"{propertyName} must be in UTC.");
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

        if (arrivalTimeUtc.Kind != DateTimeKind.Utc)
        {
            throw new DateException(arrivalTimeUtc, $"{propertyName} must be in UTC.");
        }

        if (arrivalTimeUtc <= departureTimeUtc)
        {
            throw new DateException(arrivalTimeUtc, $"{propertyName} must occur after departure time.");
        }

        return arrivalTimeUtc;
    }

    
}