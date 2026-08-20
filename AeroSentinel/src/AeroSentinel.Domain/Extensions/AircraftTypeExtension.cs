namespace AeroSentinel.Domain.Extensions;

public static class AircraftTypeExtension
{
    public static AircraftManufacturer GetManufacturer(
        this AircraftType aircraftType)
    {
        return aircraftType switch
        {
            AircraftType.A320 => AircraftManufacturer.Airbus,
            AircraftType.A321 => AircraftManufacturer.Airbus,
            AircraftType.A330 => AircraftManufacturer.Airbus,
            AircraftType.A350 => AircraftManufacturer.Airbus,

            AircraftType.B777 => AircraftManufacturer.Boeing,

            AircraftType.ATR72 => AircraftManufacturer.ATR,

            _ => throw new InvalidOperationException(
                $"Unsupported aircraft type: {aircraftType}")
        };
    }
}