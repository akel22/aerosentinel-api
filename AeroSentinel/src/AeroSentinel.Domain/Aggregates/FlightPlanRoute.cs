using AeroSentinel.Domain.Validations;

namespace AeroSentinel.Domain.Aggregates
{
    public sealed class FlightPlanRoute
    {
        public Guid FlightPlanId { get; init; } 
        public FlightPlan FlightPlan {get; init;} = null!;
         public int Sequence { get; private set; }
        public string WaypointId { get; init;} = null!;

        public Waypoint Waypoint  {get; init;} = null!;

        private FlightPlanRoute()
        {
            // Parameterless constructor for ORM and serialization
        }
        public FlightPlanRoute(Guid flightPlanId, string waypointId, int sequence)
        {
            FlightPlanId = FlightTelemetryValidation.RequireValidGuid(Guid.NewGuid(), null);
            Sequence = RouteValidation.RequireValidSequence(sequence, nameof(sequence));
            WaypointId = RouteValidation.RequireValidWaypoint(waypointId, nameof(waypointId)); //VALIDATIONS

        }
    }
}