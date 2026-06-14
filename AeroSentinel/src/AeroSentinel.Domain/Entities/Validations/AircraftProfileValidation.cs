namespace AeroSentinel.Domain.Entities.Validations;
public static class AircraftProfileValidation
{
    public static double RequirePositive(double variable, string variableName)
    {
        if (variable <= 0)
            throw new ArgumentOutOfRangeException(
                variableName,
                $"{variableName} must be greater than zero.");

        return variable;
    }

    public static string RequirePhilippineRegistration(
    string registration, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(registration))
        {
            throw new InvalidAircraftRegistrationException(
                registration,
                $"{propertyName} is required.");
        }

        registration = registration.Trim().ToUpperInvariant();

        if (!Regex.IsMatch(registration, @"^RP-C[A-Z0-9]{4}$"))
        {
            throw new InvalidAircraftRegistrationException(
                registration,
                $"{propertyName} must follow the format RP-C####.");
        }

        return registration;
    }

    public static string RequireValidAircraftID(string aircraftId, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(aircraftId))
        {
            throw new InvalidAircraftIdentifierException(
                aircraftId,
                $"{propertyName} is required and does not contain spaces.");
        }

        aircraftId = aircraftId.Trim().ToUpperInvariant();

      return aircraftId;
    }

    public static string RequireValidICAO24(string ICAO24, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(ICAO24))
        {
            throw new InvalidICAOCodeException(
                ICAO24,
                $"{propertyName} is required and does not contain spaces.");
        }

        ICAO24 = ICAO24.Trim().ToUpperInvariant();
        
          if (!Regex.IsMatch(ICAO24, "^[A-F0-9]{6}$"))
        {
            throw new InvalidICAOCodeException(ICAO24,
                "ICAO24 must contain exactly 6 hexadecimal characters.");
        }

        return ICAO24;
    }

    public static AircraftType RequireValidAircraftTypeCode(string aircraftTypeCode, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(aircraftTypeCode))
            throw new InvalidAircraftTypeException(
                aircraftTypeCode,
                $"{propertyName} cannot be empty.");

        aircraftTypeCode = aircraftTypeCode.Trim().ToUpperInvariant();

        return aircraftTypeCode switch
        {
            "A320" => AircraftType.A320,
            "A321" => AircraftType.A321,
            "A330" => AircraftType.A330,
            "A350" => AircraftType.A350,

            "B777" => AircraftType.B777,

            "ATR72" => AircraftType.ATR72,

            _ => throw new InvalidAircraftTypeException(
                aircraftTypeCode,
                $"{propertyName} is not a valid aircraft type.")
        };
    }


}
