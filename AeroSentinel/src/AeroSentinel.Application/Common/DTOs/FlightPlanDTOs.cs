namespace AeroSentinel.Application.Common.DTOs;

public sealed record CreateFlightPlanDTO(
    string ICAO24,
    string Callsign,
    string DepartureAirport,
    string DestinationAirport,
    DateTime DepartureTimeUtc,
    DateTime EstimatedArrivalTimeUtc);

public sealed record AddFlightPlanRouteDTO(string WaypointId, int Sequence);
