namespace AeroSentinel.Domain.Extensions;

public enum TelemetryStatus
{
    Verified = 1,
    Compromised = 2,
    Spoofed = 3,    
    PendingVerification = 4
}

public enum CredentialStatus
{
    Active = 1,
    Suspended = 2,
    Revoked = 3
}


public enum AircraftType
{
    // Airbus
    A320 = 1,
    A321 = 2,
    A330 = 3,
    A350 = 4,

    // Boeing
    B777 = 5,
    // ATR
    ATR72 = 6

}

public enum AircraftManufacturer
{
    Airbus = 1,
    Boeing = 2,
    ATR = 3
}