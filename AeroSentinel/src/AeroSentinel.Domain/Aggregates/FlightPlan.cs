public sealed class FlightPlan
{
    public Guid FlightPlanID { get; private set; } 
    public string ICAO24 { get; private set; } = null!; //FK to AircraftProfile.ICAO24

    public string Callsign { get; private set; } = null!;
    public string DepartureAirport { get; private set; } = null!;
    public string DestinationAirport { get; private set; } = null!;

    public DateTime DepartureTimeUtc { get; private set; } 
    public DateTime EstimatedArrivalTimeUtc { get; private set; } 
    public string CurrentWaypoint { get; private set; } = null!;
    public string NextWaypoint { get; private set; } = null!;


    private FlightPlan()
    {
        
    }

    public FlightPlan(string icao24, string callsign, string departureAirport, string destinationAirport, DateTime departureTimeUtc, 
    DateTime estimatedArrivalTimeUtc, string currentWaypoint, string nextWaypoint)
    {
        FlightPlanID = Guid.NewGuid();

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

        CurrentWaypoint = PhilippineFlightPlanDetailsValidation.RequireValidWaypoint
        (currentWaypoint, nameof(currentWaypoint));

        NextWaypoint = PhilippineFlightPlanDetailsValidation.RequireValidWaypoint
        (nextWaypoint, nameof(nextWaypoint));

    }
}