namespace AeroSentinel.Domain.Validations
{
    public static class RouteValidation
    {
        public static string RequireValidWaypoint(string waypointId, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(waypointId))
            {

                throw new InvalidWaypointException(
                waypointId, "Cannot be empty");
            }

            waypointId = waypointId.Trim().ToUpperInvariant();

            if (!Regex.IsMatch(waypointId, @"^[A-Z0-9]{2,6}$"))
            {
                throw new InvalidWaypointException(
                   propertyName, "Must be a valid Philippine Waypoint from the CSV");
            }

            return waypointId;
        }


        public static int RequireValidSequence(int sequence, string propertyName)
        {
           if(sequence < 1)
            {
                throw new InvalidRouteException(sequence, "Route sequence must start at 1");
            }

            return sequence;
        }
    }
}