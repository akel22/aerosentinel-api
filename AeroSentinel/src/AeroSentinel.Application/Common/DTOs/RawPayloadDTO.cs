namespace AeroSentinel.Application.Common.DTOs;
public record RawPayloadDTO(
    string Icao24, 
    string Callsign, 
    double Latitude, 
    double Longitude,
     double Altitude, 
     double Velocity, 
     double Heading, 
     DateTime Timestamp);