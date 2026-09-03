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

        var credentials = await _aircraftCredentialRepository.GetAllAsync(cancellationToken);

        var flightPlans = await _flightPlanRepository.GetLatestAsync(10, cancellationToken);

        // Define constants/configuration parameters for pagination limits if needed
       // Helper calculation for rate percentages avoiding divide-by-zero
        double authRate = telemetry.Count > 0 
            ? Math.Round((double)telemetry.Count(x => x.Status == TelemetryStatus.Finished) / telemetry.Count * 100, 2) 
            : 0.0;

        return new DashboardSnapshotDTO(

        Summary: new DashboardSummaryDTO(
            RegisteredAircraft: profiles.Count,
            TelemetryFrames: telemetry.Count,
            VerifiedFrames: telemetry.Count(x => x.Status == TelemetryStatus.Ongoing || x.Status == TelemetryStatus.Finished),
            FailedFrames: telemetry.Count(x => x.Status == TelemetryStatus.Spoofed),
            CredentialedAircraft: credentials.Count,
            AuthenticationRate: authRate,
            ActiveAircraft: profiles.Count
        ),

        Aircraft: [.. telemetry
            .DistinctBy(x => x.Callsign)
            .Select(frame => new AircraftStatusDTO(
                Callsign: frame.Callsign,
                Icao24: frame.ICAO24,
                Status: frame.Status.ToString(),
                LastTelemetry: FormatTimestamp(frame.TimestampUtc),
                Authentication: frame.Status.ToString(),
                Altitude: frame.SpatialState.GeoAltitudeFeet,
                Speed: frame.SpatialState.GroundSpeedKnots
            ))],


        LatestTelemetry: [.. telemetry
            .OrderByDescending(x => x.TimestampUtc)
            .Select(frame => new LatestTelemetryDTO(
                MessageId: frame.MessageId,
                Callsign: frame.Callsign,
                Icao24: frame.ICAO24,
                Status: frame.Status.ToString(),
                TimestampUtc: frame.TimestampUtc,
                SequenceNumber: frame.SequenceNumber,
                FailureReason: frame.FailureReason ?? string.Empty
            ))],

        FinishedFlights: [.. flightPlans
            .Where(x => x.FlightStatus == FlightStatus.Finished)
            .Select(plan => new FinishedFlightDTO(
                FlightPlanId: plan.FlightPlanId,
                Callsign: plan.Callsign,
                Icao24: plan.ICAO24,
                DepartureAirport: plan.DepartureAirport,
                DestinationAirport: plan.DestinationAirport,
                EstimatedArrivalTimeUtc: plan.EstimatedArrivalTimeUtc,
                ActualArrivalTimeUtc: plan.ActualArrivalTimeUtc,
                ArrivalVarianceMinutes: TimeOnly.FromTimeSpan(
                    plan.ActualArrivalTimeUtc > plan.EstimatedArrivalTimeUtc 
                        ? plan.ActualArrivalTimeUtc - plan.EstimatedArrivalTimeUtc 
                        : plan.EstimatedArrivalTimeUtc - plan.ActualArrivalTimeUtc)
            ))]
        );
}

    private static string FormatTimestamp(DateTime timestamp) =>
        timestamp.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
}