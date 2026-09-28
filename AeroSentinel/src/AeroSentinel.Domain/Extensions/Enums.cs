namespace AeroSentinel.Domain.Extensions;

public enum TelemetryStatus
{
    Ongoing,
    Verified = Ongoing,
    Spoofed,
    Finished,

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

public enum FlightStatus 
{
    Ongoing,
    Finished,
    Cancelled

}