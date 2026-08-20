namespace AeroSentinel.Domain.ValueObjects;
public sealed class FlightIntent
{
    public double VerticalRateFpm { get; init;}
    public double SelectedAltitudeFeet { get; init; }
    public double IndicatedAirspeedKnots { get; init; }
    public double MagneticHeadingDegrees { get; init;}
    public double RollAngleDegrees { get; init;}

    private FlightIntent()
    {
        
    
    }

    public FlightIntent(double verticalRateFpm, double selectedAltitudeFeet, double indicatedAirspeedKnots, double magneticHeadingDegrees, double rollAngleDegrees)
    {
        VerticalRateFpm = FlightTelemetryValidation.RequireValidVerticalRate(verticalRateFpm, nameof(verticalRateFpm));
        SelectedAltitudeFeet = FlightTelemetryValidation.RequireValidSelectedAutoPilotAltitudeFeet(selectedAltitudeFeet, nameof(selectedAltitudeFeet));
        IndicatedAirspeedKnots = FlightTelemetryValidation.RequireValidIndicatedAirspeed(indicatedAirspeedKnots, nameof(indicatedAirspeedKnots));
        MagneticHeadingDegrees = FlightTelemetryValidation.RequireValidMagneticHeading(magneticHeadingDegrees, nameof(magneticHeadingDegrees));
        RollAngleDegrees = FlightTelemetryValidation.RequireValidRollAngle(rollAngleDegrees, nameof(rollAngleDegrees));
   
    }
}