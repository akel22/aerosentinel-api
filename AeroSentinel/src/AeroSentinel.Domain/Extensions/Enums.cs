namespace AeroSentinel.Domain.Extensions;

public enum TelemetryStatus
{
    Verified,
    Compromised,
    Spoofed = 3,    
    PendingVerification
}

public enum CredentialStatus
{
    Active,
    Inactive
}


public enum AircraftType
{
    // Airbus
    A320,
    A321,
    A330,
    A350,

    // Boeing
    B777,
    // ATR
    ATR72

}

public enum AircraftManufacturer
{
    Airbus,
    Boeing,
    ATR
}