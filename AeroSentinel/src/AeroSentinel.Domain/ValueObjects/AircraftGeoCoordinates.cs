namespace AeroSentinel.Domain.ValueObjects;

public readonly record struct AircraftGeoCoordinates
{
    public double Latitude { get; }
    public double Longitude { get; }

    public AircraftGeoCoordinates(double latitude, double longitude) 
    {
        Latitude = FlightTelemetryValidation.RequireValidLatitude(latitude, nameof(latitude));
        Longitude = FlightTelemetryValidation.RequireValidLongitude(Longitude, nameof(longitude));
        
    }
}