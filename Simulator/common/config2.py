"""Runtime configuration for the second legitimate telemetry simulator.

Set the SIMULATOR2 values to the credentials and flight plan for a second
aircraft registered through the API.  These variables are intentionally
separate from SIMULATOR_* so both legitimate simulators can run together.
"""

from __future__ import annotations

import os
from uuid import UUID


API_BASE_URL = os.getenv("SIMULATOR2_API_BASE_URL", "http://localhost:5253").rstrip("/")
ICAO24 = os.getenv("SIMULATOR2_ICAO24", "758002").upper()
CALLSIGN = os.getenv("SIMULATOR2_CALLSIGN", "CEB123").upper()
SECRET_KEY_HEX = os.getenv("SIMULATOR2_SECRET_HEX", "112233445566778899aabbccddeeff00112233445566778899aabbccddeeff00")
FLIGHT_PLAN_ID = UUID(os.getenv("SIMULATOR2_FLIGHT_PLAN_ID", "C295F5A6-62DF-490F-BB40-C8033EE4D1DD"))
INTERVAL_SECONDS = float(os.getenv("SIMULATOR2_INTERVAL_SECONDS", "10"))
