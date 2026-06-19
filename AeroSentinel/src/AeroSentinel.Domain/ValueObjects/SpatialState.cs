namespace AeroSentinel.Domain.ValueObjects;

public readonly record struct SpatialState
{
    public AircraftGeoCoordinates Coordinates { get; } 
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
}