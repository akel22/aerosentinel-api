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
SECRET_KEY_HEX = os.getenv("SIMULATOR_SECRET_HEX", "bcdbe32d26e0bdd220aca6d0dbf88046ea2ed232f8c8a43777cf30734640db3f")
FLIGHT_PLAN_ID = UUID(os.getenv("SIMULATOR_FLIGHT_PLAN_ID", "8A907673-F884-42A0-AB82-D14DF70A318D"))
INTERVAL_SECONDS = float(os.getenv("SIMULATOR_INTERVAL_SECONDS", "10"))
