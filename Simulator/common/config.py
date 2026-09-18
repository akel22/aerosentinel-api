"""Runtime configuration for the telemetry simulators.

Set SIMULATOR_SECRET_HEX to the 32-byte verification key for ICAO24 in the API
database, and SIMULATOR_FLIGHT_PLAN_ID to a flight-plan ID created through the API.
"""

from __future__ import annotations

import os
from uuid import UUID


API_BASE_URL = os.getenv("SIMULATOR_API_BASE_URL", "http://localhost:5253").rstrip("/")
ICAO24 = os.getenv("SIMULATOR_ICAO24", "758001").upper()
CALLSIGN = os.getenv("SIMULATOR_CALLSIGN", "CEB123").upper()
SECRET_KEY_HEX = os.getenv("SIMULATOR_SECRET_HEX", "A73A33C0C55A04C22A0CF0A32A354D19")
FLIGHT_PLAN_ID = UUID(os.getenv("SIMULATOR_FLIGHT_PLAN_ID", "c047f9c7-e1c2-4481-9f42-85c77cb6eaae"))
INTERVAL_SECONDS = float(os.getenv("SIMULATOR_INTERVAL_SECONDS", "10"))
