namespace AeroSentinel.Application.Common.DTOs;
public record RawPayloadDTO(
    string Icao24, 
    string Callsign, 
    string Registration,
    double Latitude, 
    double Longitude,
     double Altitude, 
     double Velocity,  
     DateTime Timestamp);