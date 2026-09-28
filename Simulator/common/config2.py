"""Runtime configuration for the second legitimate telemetry simulator.

Set the SIMULATOR2 values to the credentials and flight plan for a second
aircraft registered through the API.  These variables are intentionally
separate from SIMULATOR_* so both legitimate simulators can run together.
"""

from __future__ import annotations

import os
from uuid import UUID


API_BASE_URL = os.environ["SIMULATOR2_API_BASE_URL"].rstrip("/")
ICAO24 = os.environ["SIMULATOR2_ICAO24"].upper()
CALLSIGN = os.environ["SIMULATOR2_CALLSIGN"].upper()
SECRET_KEY_HEX = os.environ["SIMULATOR2_SECRET_HEX"]
FLIGHT_PLAN_ID = UUID(os.environ["SIMULATOR2_FLIGHT_PLAN_ID"])
INTERVAL_SECONDS = float(os.environ["SIMULATOR2_INTERVAL_SECONDS"])
