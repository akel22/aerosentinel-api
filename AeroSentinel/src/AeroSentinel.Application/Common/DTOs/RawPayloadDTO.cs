namespace AeroSentinel.Application.Common.DTOs;
public record RawPayloadDTO(
    string ICAO24, 
    string Callsign, 
    double Latitude, 
    double Longitude,
     double Altitude, 
     double Velocity,  
     DateTime Timestamp);