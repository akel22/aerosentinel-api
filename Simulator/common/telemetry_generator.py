"""Generates a smooth, realistic-looking stream of aircraft telemetry."""

from __future__ import annotations

from datetime import datetime, timezone
from math import cos, pi, sin
from uuid import UUID


class TelemetryGenerator:
    def __init__(self, flight_plan_id: UUID, icao24: str, callsign: str) -> None:
        self._flight_plan_id = str(flight_plan_id)
        self._icao24 = icao24
        self._callsign = callsign
        self._sequence = 0
        self._phase = 0.0

    def next_payload(self) -> dict[str, object]:
        self._sequence += 1
        self._phase += 0.012

        # A small eastbound track near Manila at cruise altitude.
        latitude = 14.5995 + 0.08 * sin(self._phase)
        longitude = 120.9842 + 0.12 * cos(self._phase)
        altitude = 34_000.0 + 120.0 * sin(self._phase * 0.5)
        track = (90.0 + 6.0 * sin(self._phase)) % 360.0

        return {
            "FlightPlanId": self._flight_plan_id,
            "Sequence": self._sequence,
            "ICAO24": self._icao24,
            "Callsign": self._callsign,
            "Squawk": "2145",
            "TimestampUTC": datetime.now(timezone.utc).isoformat(timespec="milliseconds").replace("+00:00", "Z"),
            "Latitude": latitude,
            "Longitude": longitude,
            "BaroAltitudeFeet": altitude,
            "GeoAltitudeFeet": altitude - 20.0,
            "GroundSpeedKnots": 445.0 + 3.0 * sin(self._phase),
            "TrackAngleDegrees": track,
            "VerticalRateFpm": 0.0,
            "SelectedAltitudeFeet": 34_000.0,
            "IndicatedAirspeedKnots": 285.0,
            "MagneticHeadingDegrees": track - 1.5,
            "RollAngleDegrees": 1.2 * sin(self._phase),
        }
