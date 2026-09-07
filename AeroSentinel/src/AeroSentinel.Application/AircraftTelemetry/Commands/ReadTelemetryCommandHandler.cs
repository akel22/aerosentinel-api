using System.Globalization;
using AeroSentinel.Domain.Extensions;

namespace AeroSentinel.Application.AircraftTelemetry.Commands;

public class ReadTelemetryCommandHandler : IRequestHandler<ReadTelemetryCommand, DashboardSnapshotDTO>
{
    private readonly IAircraftTelemetryRepository _aircraftTelemetryRepository;
    private readonly IAircraftProfileRepository _aircraftProfileRepository;
    private readonly IAircraftCredentialRepository _aircraftCredentialRepository;
    private readonly IFlightPlanRepository _flightPlanRepository;

    public ReadTelemetryCommandHandler(
        IAircraftTelemetryRepository aircraftTelemetryRepository,
        IAircraftProfileRepository aircraftProfileRepository,
        IAircraftCredentialRepository aircraftCredentialRepository,
        IFlightPlanRepository flightPlanRepository)
    {
        _aircraftTelemetryRepository = aircraftTelemetryRepository;
        _aircraftProfileRepository = aircraftProfileRepository;
        _aircraftCredentialRepository = aircraftCredentialRepository;
        _flightPlanRepository = flightPlanRepository;
    }

    public async Task<DashboardSnapshotDTO> Handle(ReadTelemetryCommand request, CancellationToken cancellationToken)
    {
        var profiles = await _aircraftProfileRepository.GetAllAsync(cancellationToken);

        var telemetry = (await _aircraftTelemetryRepository.GetAllAsync(cancellationToken))
            .Where(frame => frame is not null)
            .Select(frame => frame!)
            .OrderByDescending(frame => frame.TimestampUtc)
            .ToList();
        
        var telemetryLatestTimestamp = await _aircraftTelemetryRepository.GetLastFlightTelemetryTimestampAsync(cancellationToken);

        var finishedFlights = await _flightPlanRepository.GetFininishedFlightsAsync(cancellationToken);

        var credentials = await _aircraftCredentialRepository.GetAllAsync(cancellationToken);

        // Define constants/configuration parameters for pagination limits if needed
       // Helper calculation for rate percentages avoiding divide-by-zero
        double verifPercent = telemetry.Count > 0 ? 
            Math.Round((double)(telemetry.Count(x => x.Status == TelemetryStatus.Ongoing ||
                x.Status == TelemetryStatus.Finished) / telemetry.Count) * 100, 2) : 0.0;

        return new DashboardSnapshotDTO(
            
        Timestamp : telemetryLatestTimestamp!.TimestampUtc,

        Summary: new DashboardSummaryDTO(
            AircraftProfilesCount: profiles.Count,
            TotalTelemetry: telemetry.Count,
            VerifiedTelemetry: telemetry.Count(x => x.Status == TelemetryStatus.Ongoing || x.Status == TelemetryStatus.Finished),
            FailedTelemetry: telemetry.Count(x => x.Status == TelemetryStatus.Spoofed),
            CredentialsCount: credentials.Count,
            VerificationPercentage: verifPercent,
            ActiveAircraft: profiles.Count
        ), [],

        AircraftStatus: [.. telemetry
            .DistinctBy(x => x.Callsign)
            .Select(frame => new LatestAircraftStatusDTO(
                Callsign: frame.Callsign,
                Icao24: frame.ICAO24,
                Status: frame.Status.ToString(),
                LastTelemetry: FormatTimestamp(frame.TimestampUtc),
                Authentication: frame.Status.ToString(),
                Altitude: frame.SpatialState.GeoAltitudeFeet,
                Speed: frame.SpatialState.GroundSpeedKnots
            ))],[],[],


        LatestTelemetry: [new LatestTelemetryDTO(
                MessageId: telemetryLatestTimestamp.MessageId,
                Callsign: telemetryLatestTimestamp.Callsign,
                Icao24: telemetryLatestTimestamp.ICAO24,
                Status: telemetryLatestTimestamp.Status.ToString(),
                Timestamp: telemetryLatestTimestamp.TimestampUtc,
                SequenceNumber: telemetryLatestTimestamp.SequenceNumber,
                FailureReason: telemetryLatestTimestamp.FailureReason ?? string.Empty
            )],

        FinishedFlights: [.. finishedFlights.Select(flight => new FinishedFlightDTO(
                FlightPlanId: flight.FlightPlanId,
                Callsign: flight.Callsign,
                Icao24: flight.ICAO24,
                DepartureAirport: flight.DepartureAirport,
                DestinationAirport: flight.DestinationAirport,
                EstimatedArrivalTimeUtc: flight.EstimatedArrivalTimeUtc,
                ActualArrivalTimeUtc: flight.ActualArrivalTimeUtc,
                DelayMinutes: TimeOnly.FromTimeSpan(
                    flight.ActualArrivalTimeUtc > flight.EstimatedArrivalTimeUtc 
                        ? flight.ActualArrivalTimeUtc - flight.EstimatedArrivalTimeUtc 
                        : flight.EstimatedArrivalTimeUtc - flight.ActualArrivalTimeUtc).
                        ToShortTimeString()))]
        );
    
}

    private static string FormatTimestamp(DateTime timestamp) =>
        timestamp.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
}