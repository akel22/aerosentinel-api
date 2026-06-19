public sealed class FlightPlan
{
    public Guid FlightPlanID { get; init;} 
    public string ICAO24 { get; init;} = null!; //FK to AircraftProfile.ICAO24

    public string Callsign { get; init;} = null!;
    public string DepartureAirport { get; init; } = null!;
    public string DestinationAirport { get; init;} = null!;

    public DateTime DepartureTimeUtc { get; init;} 
    public DateTime EstimatedArrivalTimeUtc { get; init;} 
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