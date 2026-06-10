namespace AeroSentinel.Domain.Enums;

public enum TelemetryStatus
{
    Verified = 1,
    Compromised = 2,
    Spoofed = 3,    
    Stale = 4
}

public enum AircraftType
{
    // Airbus
    A320 = 1,
    A321 = 2,
    A330 = 3,
    A350 = 4,

    B747 = 5,

    // Boeing
    B737 = 6,
    B777 = 7,
    B787 = 8,

    // ATR
    ATR72 = 9,

    // De Havilland Canada
    DHC8Q400 = 10,

    // Embraer
    E190 = 11
}

public enum AircraftManufacturer
{
    Airbus = 1,
    Boeing = 2,
    ATR = 3,
    DeHavillandCanada = 4,
    Embraer = 5
}