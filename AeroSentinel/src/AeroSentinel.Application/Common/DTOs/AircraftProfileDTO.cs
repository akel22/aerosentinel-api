namespace AeroSentinel.Application.Common.DTOs;

public record AircraftProfileDTO(

     string ICAO24 ,
     string Registration ,
     string AircraftType ,
     string Manufacturer , 
     double CruiseSpeedKnots,
     double MaxVelocityKnots,
     double MaxAltitudeFeet ,
     double MaxClimbRateFeetPerMinute ,
     double MaxDescentRateFeetPerMinute ,
     double MaxTurnRateDegreesPerSecond 

    );