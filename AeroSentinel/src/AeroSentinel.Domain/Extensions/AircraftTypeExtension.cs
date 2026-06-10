using AeroSentinel.Domain.Enums;

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

            AircraftType.B737 => AircraftManufacturer.Boeing,
            AircraftType.B777 => AircraftManufacturer.Boeing,
            AircraftType.B787 => AircraftManufacturer.Boeing,

            AircraftType.ATR72 => AircraftManufacturer.ATR,

            AircraftType.DHC8Q400 =>
                AircraftManufacturer.DeHavillandCanada,

            AircraftType.E190 =>
                AircraftManufacturer.Embraer,

            _ => throw new InvalidOperationException(
                $"Unsupported aircraft type: {aircraftType}")
        };
    }
}