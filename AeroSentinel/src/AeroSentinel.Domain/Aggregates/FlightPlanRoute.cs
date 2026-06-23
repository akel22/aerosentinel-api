using System.ComponentModel.DataAnnotations.Schema;
using AeroSentinel.Domain.Aggregates;

public sealed class FlightPlanRoute
{
    public Guid RouteId {get; private set;}

    public Guid FlightPlanId {get; private set;}

    public string WaypointId {get; private set;}

     [ForeignKey("WaypointId")] public Waypoints Waypoints = null!;

    public int Sequence {get; private set;}


    public FlightPlanRoute(Guid routeId, Guid flightPlanId, string waypointId,
                                int sequence)
    {
        RouteId = routeId;
        FlightPlanId = flightPlanId;
        WaypointId = waypointId; //VALIDATIONS
        Sequence = sequence;


    }


    
}