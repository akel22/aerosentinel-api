public sealed class FlightPlan
{
    public string DepartureAirport { get; private set; }
    public string DestinationAirport { get; private set; }
    public string CurrentWaypoint { get; private set; }
    public string NextWaypoint { get; private set; }

    public FlightPlan(
        string departureAirport,
        string destinationAirport,
        string currentWaypoint,
        string nextWaypoint)
    {
        DepartureAirport = FlightPlanDetailsValidation.RequireValidAirportCode
       (departureAirport, nameof(departureAirport));

        DestinationAirport = FlightPlanDetailsValidation.RequireValidAirportCode
        (destinationAirport, nameof(destinationAirport));

        CurrentWaypoint = FlightPlanDetailsValidation.RequireValidWaypoint
        (currentWaypoint, nameof(currentWaypoint));

        NextWaypoint = FlightPlanDetailsValidation.RequireValidWaypoint
        (nextWaypoint, nameof(nextWaypoint));

    }
}