namespace AeroSentinel.Domain.ValueObjects;

public sealed class AircraftGeoCoordinates
{
    public double Latitude { get; init;}
    public double Longitude { get; init;}

    public AircraftGeoCoordinates(double latitude, double longitude) 
    {
        Latitude = FlightTelemetryValidation.RequireValidLatitude(latitude, nameof(latitude));
        Longitude = FlightTelemetryValidation.RequireValidLongitude(longitude, nameof(longitude));
        
    }
}