namespace AeroSentinel.Application.Common.DTOs;

public record AircraftProfileDTO(

     string ICAO24 ,
     string Registration ,
     string AircraftType ,
     string Manufacturer , 
     double CruiseSpeedKnots,
     double MaxVelocityKnots, 
     Guid CredentialId ,
     double MaxAltitudeFeet ,
     double MaxClimbRateFeetPerMinute ,
     double MaxDescentRateFeetPerMinute ,
     double MaxTurnRateDegreesPerSecond 

    );