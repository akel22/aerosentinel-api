namespace AeroSentinel.Domain.Aggregates;

public sealed class Waypoints{

    public string WaypointId {get; init;}

    public double Latitude {get; init;}

    public double Longitude {get; init;}

        public Waypoints(string waypointId, double latitude, double longitude)
        {
            WaypointId = waypointId;
            Latitude = latitude; //VALIDATIONS
            Longitude = longitude;

        }
}