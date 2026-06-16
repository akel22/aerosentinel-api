namespace AeroSentinel.Domain.ValueObjects;

public sealed class AircraftGeoCoordinates
{
    public double Latitude { get; }
    public double Longitude { get; }

    public AircraftGeoCoordinates(double latitude, double longitude) 
    {
        Latitude = FlightTelemetryValidation.RequireValidLatitude(latitude, nameof(latitude));
        Longitude = FlightTelemetryValidation.RequireValidLongitude(latitude, nameof(longitude));;
    }

    public override bool Equals(object? obj)
    {
        return obj is AircraftGeoCoordinates coordinates &&
               Latitude == coordinates.Latitude &&
               Longitude == coordinates.Longitude;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude);
    }

    public static bool operator ==(AircraftGeoCoordinates? left, AircraftGeoCoordinates? right)
    {
        return EqualityComparer<AircraftGeoCoordinates>.Default.Equals(left, right);
    }

    public static bool operator !=(AircraftGeoCoordinates? left, AircraftGeoCoordinates? right)
    {
        return !(left == right);
    }
}