"""Runtime configuration for the telemetry simulators.

Set SIMULATOR_SECRET_HEX to the 32-byte verification key for ICAO24 in the API
database, and SIMULATOR_FLIGHT_PLAN_ID to a flight-plan ID created through the API.
"""

from __future__ import annotations

import os
from uuid import UUID


API_BASE_URL = os.getenv("SIMULATOR_API_BASE_URL", "http://localhost:5253").rstrip("/")
ICAO24 = os.getenv("SIMULATOR_ICAO24", "758802").upper()
CALLSIGN = os.getenv("SIMULATOR_CALLSIGN", "PAL123").upper()
SECRET_KEY_HEX = os.getenv("SIMULATOR_SECRET_HEX", "")
FLIGHT_PLAN_ID = UUID(os.getenv("SIMULATOR_FLIGHT_PLAN_ID", "00000000-0000-0000-0000-000000000000"))
INTERVAL_SECONDS = float(os.getenv("SIMULATOR_INTERVAL_SECONDS", "10"))
