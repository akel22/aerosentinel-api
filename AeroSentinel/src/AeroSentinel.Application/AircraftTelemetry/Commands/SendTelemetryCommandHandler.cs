
using AeroSentinel.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace AeroSentinel.Application.AircraftTelemetry.Commands;

public class SendTelemetryCommandHandler : IRequestHandler<SendTelemetryCommand, Guid>
{
        private readonly IAircraftProfileRepository _aircraftProfileRepository;
    private readonly IAircraftTelemetryRepository _aircraftTelemetryRepository;

    private readonly Logger<SendTelemetryCommandHandler> _logger;
    public SendTelemetryCommandHandler( 

            IAircraftProfileRepository aircraftProfileRepository,
            IAircraftTelemetryRepository aircraftTelemetryRepository,
            Logger<SendTelemetryCommandHandler> logger)
    {
        _aircraftProfileRepository = aircraftProfileRepository;
        _aircraftTelemetryRepository = aircraftTelemetryRepository;
        _logger = logger;
    }

    public async Task<Guid> Handle(SendTelemetryCommand request, CancellationToken cancellationToken)
    {
      var rawPayload = request.Payload;

      var coordinates = new AircraftGeoCoordinates(rawPayload.Latitude, rawPayload.Longitude);
      var spatialState = new SpatialState
      (
        coordinates,
        rawPayload.BaroAltitudeFeet,
        rawPayload.GeoAltitudeFeet,
        rawPayload.GroundSpeedKnots,
        rawPayload.TrackAngleDegrees
      );

       var flightIntent = new FlightIntent
       (
        rawPayload.VerticalRateFpm,
        rawPayload.SelectedAltitudeFeet,
        rawPayload.IndicatedAirspeedKnots,
        rawPayload.MagneticHeadingDegrees, 
        rawPayload.RollAngleDegrees
       );


      var flightTelemetry = new FlightTelemetry
      (
          rawPayload.ICAO24!,
          rawPayload.Callsign!,
          rawPayload.Squawk!,
          rawPayload.TimestampUTC,
          spatialState,
          flightIntent,
          rawPayload.Sequence,
          rawPayload.Signature
        
      );

       
    
      
    }
}