"""Runtime configuration for the telemetry simulators.

Set SIMULATOR_SECRET_HEX to the 32-byte verification key for ICAO24 in the API
database, and SIMULATOR_FLIGHT_PLAN_ID to a flight-plan ID created through the API.
"""

from __future__ import annotations

import os
from uuid import UUID


API_BASE_URL = os.environ["SIMULATOR_API_BASE_URL"].rstrip("/")
ICAO24 = os.environ["SIMULATOR_ICAO24"].upper()
CALLSIGN = os.environ["SIMULATOR_CALLSIGN"].upper()
SECRET_KEY_HEX = os.environ["SIMULATOR_SECRET_HEX"]
FLIGHT_PLAN_ID = UUID(os.environ["SIMULATOR_FLIGHT_PLAN_ID"])
INTERVAL_SECONDS = float(os.environ["SIMULATOR_INTERVAL_SECONDS"])
