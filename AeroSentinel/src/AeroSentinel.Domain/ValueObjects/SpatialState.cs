namespace AeroSentinel.Domain.ValueObjects;

public sealed class SpatialState
{
    public AircraftGeoCoordinates Coordinates { get; } // Reusing your Coordinate class!
    public double BaroAltitudeFeet { get; }
    public double GeoAltitudeFeet { get; }
    public double GroundSpeedKnots { get; }
    public double TrackAngleDegrees { get; }

    public SpatialState(AircraftGeoCoordinates coordinates, double baroAltitudeFeet, double geoAltitudeFeet, double groundSpeedKnots, double trackAngleDegrees)
    {
        Coordinates = FlightTelemetryValidation.RequireValidCoord(coordinates, nameof(coordinates));
        BaroAltitudeFeet = FlightTelemetryValidation.RequireValidBaroAltitude(baroAltitudeFeet, nameof(baroAltitudeFeet));
        GeoAltitudeFeet = FlightTelemetryValidation.RequireValidSelectedAutoPilotAltitudeFeet(geoAltitudeFeet, nameof(geoAltitudeFeet));
        GroundSpeedKnots = FlightTelemetryValidation.RequireValidGroundSpeedKnots(groundSpeedKnots, nameof(groundSpeedKnots));
        TrackAngleDegrees = FlightTelemetryValidation.RequireValidTrackAngleDegrees(trackAngleDegrees, nameof(trackAngleDegrees));
    }
    public override bool Equals(object? obj)
    {
        return obj is SpatialState state &&
               Coordinates == state.Coordinates && // Triggers AircraftGeoCoordinates custom == operator
               BaroAltitudeFeet == state.BaroAltitudeFeet &&
               GeoAltitudeFeet == state.GeoAltitudeFeet &&
               GroundSpeedKnots == state.GroundSpeedKnots &&
               TrackAngleDegrees == state.TrackAngleDegrees;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Coordinates, BaroAltitudeFeet, GeoAltitudeFeet, GroundSpeedKnots, TrackAngleDegrees);
    }

    public static bool operator ==(SpatialState? left, SpatialState? right)
    {
        return EqualityComparer<SpatialState>.Default.Equals(left, right);
    }

    public static bool operator !=(SpatialState? left, SpatialState? right)
    {
        return !(left == right);
    }
}