namespace AeroSentinel.Domain.ValueObjects;
public sealed class FlightIntent
{
    public double VerticalRateFpm { get; }
    public double SelectedAltitudeFeet { get; }
    public double IndicatedAirspeedKnots { get; }
    public double MagneticHeadingDegrees { get; }
    public double RollAngleDegrees { get; }

    public FlightIntent(double verticalRateFpm, double selectedAltitudeFeet, double indicatedAirspeedKnots, double magneticHeadingDegrees, double rollAngleDegrees)
    {
        VerticalRateFpm = FlightTelemetryValidation.RequireValidVerticalRate(verticalRateFpm, nameof(verticalRateFpm));
        SelectedAltitudeFeet = FlightTelemetryValidation.RequireValidSelectedAutoPilotAltitudeFeet(selectedAltitudeFeet, nameof(selectedAltitudeFeet));
        IndicatedAirspeedKnots = FlightTelemetryValidation.RequireValidIndicatedAirspeed(indicatedAirspeedKnots, nameof(indicatedAirspeedKnots));
        MagneticHeadingDegrees = FlightTelemetryValidation.RequireValidMagneticHeading(magneticHeadingDegrees, nameof(magneticHeadingDegrees));
        RollAngleDegrees = FlightTelemetryValidation.RequireValidRollAngle(rollAngleDegrees, nameof(rollAngleDegrees));
   
    }
}