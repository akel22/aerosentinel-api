namespace AeroSentinel.Domain.ValueObjects;

public sealed class SpatialState
{
    public AircraftGeoCoordinates? Coordinates { get; init;}
    public double BaroAltitudeFeet { get; init; }
    public double GeoAltitudeFeet { get; init; }
    public double GroundSpeedKnots { get; init;}
    public double TrackAngleDegrees { get; init;}

    private SpatialState()
    {
        
    }

    public SpatialState(AircraftGeoCoordinates coordinates, double baroAltitudeFeet, double geoAltitudeFeet, double groundSpeedKnots, double trackAngleDegrees)
    {
        Coordinates = FlightTelemetryValidation.RequireValidCoord(coordinates, nameof(coordinates));
        BaroAltitudeFeet = FlightTelemetryValidation.RequireValidBaroAltitude(baroAltitudeFeet, nameof(baroAltitudeFeet));
        GeoAltitudeFeet = FlightTelemetryValidation.RequireValidSelectedAutoPilotAltitudeFeet(geoAltitudeFeet, nameof(geoAltitudeFeet));
        GroundSpeedKnots = FlightTelemetryValidation.RequireValidGroundSpeedKnots(groundSpeedKnots, nameof(groundSpeedKnots));
        TrackAngleDegrees = FlightTelemetryValidation.RequireValidTrackAngleDegrees(trackAngleDegrees, nameof(trackAngleDegrees));
    
    }
}