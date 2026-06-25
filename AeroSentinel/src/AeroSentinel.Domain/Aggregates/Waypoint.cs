
namespace AeroSentinel.Domain.Aggregates;

public sealed class Waypoint{
    public string WaypointId {get; init;} = null!;
    public double Latitude {get; init;}
    public double Longitude {get; init;}

    private Waypoint()
    {
        
    }

    public Waypoint(string waypointId, double latitude, double longitude)
        {
            WaypointId = WaypointValidation.RequireValidWaypoint(waypointId, nameof(waypointId));
            Latitude = WaypointValidation.RequireValidLatitude(latitude, nameof(latitude));
            Longitude = WaypointValidation.RequireValidLongitude(longitude, nameof(longitude));
        }
}