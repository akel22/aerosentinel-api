"""HMAC-SHA256 signing compatible with AeroSentinel's RawPayloadSignDTO."""

from __future__ import annotations

import hashlib
import hmac
import json
from typing import Any


# Keep this order in sync with RawPayloadSignDTO in the .NET application.
SIGNED_FIELDS = (
    "FlightPlanId", "Sequence", "ICAO24", "Callsign", "Squawk", "TimestampUTC",
    "Latitude", "Longitude", "BaroAltitudeFeet", "GeoAltitudeFeet",
    "GroundSpeedKnots", "TrackAngleDegrees", "VerticalRateFpm",
    "SelectedAltitudeFeet", "IndicatedAirspeedKnots", "MagneticHeadingDegrees",
    "RollAngleDegrees",
)


def _normalise_number(value: Any) -> Any:
    """Match System.Text.Json's compact output for integral floating-point values."""
    if isinstance(value, float) and value.is_integer():
        return int(value)
    return value


def canonical_signed_json(payload: dict[str, Any]) -> bytes:
    """Create the compact PascalCase JSON bytes the API serializes before signing."""
    signed_payload = {
        field: _normalise_number(payload[field])
        for field in SIGNED_FIELDS
    }
    return json.dumps(signed_payload, separators=(",", ":"), ensure_ascii=False).encode("utf-8")


def sign_payload(payload: dict[str, Any], secret_key_hex: str) -> str:
    """Return an uppercase hexadecimal HMAC-SHA256 signature."""
    if not secret_key_hex:
        raise ValueError("SIMULATOR_SECRET_HEX must contain the aircraft verification key.")

    try:
        secret_key = bytes.fromhex(secret_key_hex)
    except ValueError as exception:
        raise ValueError("SIMULATOR_SECRET_HEX must be hexadecimal.") from exception

    return hmac.new(secret_key, canonical_signed_json(payload), hashlib.sha256).hexdigest().upper()
