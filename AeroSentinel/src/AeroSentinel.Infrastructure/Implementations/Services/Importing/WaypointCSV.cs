namespace AeroSentinel.Infrastructure.Importing;
public sealed class WaypointsCSV {
//THIS CLASS ACTS AS CONTAINER FOR READING CSV
    public string WaypointId {get; set;} = null!;
    public double Latitude {get; set;}
    public double Longitude {get; set;}

}