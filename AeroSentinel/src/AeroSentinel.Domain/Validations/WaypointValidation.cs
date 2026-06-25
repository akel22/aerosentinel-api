namespace AeroSentinel.Domain.Validations
{
    public static class WaypointValidation
    {
        // Philippine Flight Information Region (FIR) Geofencing Constants
        private const double PhMinLatitude = 4.0;
        private const double PhMaxLatitude = 22.0;
        private const double PhMinLongitude = 116.0;
        private const double PhMaxLongitude = 127.0;

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
            if (sequence < 1)
            {
                throw new InvalidRouteException(sequence, "Route sequence must start at 1");
            }

            return sequence;
        }


        public static double RequireValidLatitude(double latitude, string propertyName)
        {
            if (latitude < PhMinLatitude || latitude > PhMaxLatitude)
                throw new InvalidCoordinatesSystemException(latitude,
                    $"{propertyName} ({latitude}) falls outside the Philippine Flight Information Region (FIR) boundaries ({PhMinLatitude}°N to {PhMaxLatitude}°N).");

            return latitude;
        }


        public static double RequireValidLongitude(double longitude, string propertyName)
        {
            if (longitude < PhMinLongitude || longitude > PhMaxLongitude)
                throw new InvalidCoordinatesSystemException(longitude,
                    $"{propertyName} ({longitude}) falls outside the Philippine Flight Information Region (FIR) boundaries ({PhMinLongitude}°E to {PhMaxLongitude}°E).");

            return longitude;
        }
    }
}