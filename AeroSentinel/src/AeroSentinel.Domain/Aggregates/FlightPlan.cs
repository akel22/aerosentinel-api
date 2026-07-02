namespace AeroSentinel.Domain.Aggregates
{
    public sealed class FlightPlan
    {
        public Guid FlightPlanId { get; init; }
        public string ICAO24 { get; init; } = null!; //FK to AircraftProfile.ICAO24
        public string Callsign { get; init; } = null!;
        public string DepartureAirport { get; init; } = null!;
        public string DestinationAirport { get; init; } = null!;

        public DateTime DepartureTimeUtc { get; init; }
        public DateTime EstimatedArrivalTimeUtc { get; init; }

        private FlightPlan()
        {

        }

        public FlightPlan(string icao24, string callsign, string departureAirport, string destinationAirport, DateTime departureTimeUtc,
        DateTime estimatedArrivalTimeUtc)
        {
            FlightPlanId =  FlightTelemetryValidation.RequireValidGuid(Guid.NewGuid(), null);

            ICAO24 = AircraftProfileValidation.RequireValidICAO24(icao24, nameof(icao24));//FK

            Callsign = PhilippineFlightPlanDetailsValidation.RequireValidCallsign(callsign, nameof(callsign));

            DepartureAirport = PhilippineFlightPlanDetailsValidation.RequireValidAirportCode
           (departureAirport, nameof(departureAirport));

            DestinationAirport = PhilippineFlightPlanDetailsValidation.RequireValidAirportCode
            (destinationAirport, nameof(destinationAirport));

            DepartureTimeUtc = PhilippineFlightPlanDetailsValidation.RequireValidDepartureTime
            (departureTimeUtc, nameof(departureTimeUtc));

            EstimatedArrivalTimeUtc = PhilippineFlightPlanDetailsValidation.RequireValidArrivalTime
            (estimatedArrivalTimeUtc, departureTimeUtc, nameof(estimatedArrivalTimeUtc));

        }
    }
}