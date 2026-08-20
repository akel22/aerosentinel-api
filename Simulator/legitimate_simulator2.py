"""Continuously sends signed, realistic telemetry to AeroSentinel."""

from __future__ import annotations

import time

from common.config2 import API_BASE_URL, CALLSIGN, FLIGHT_PLAN_ID, ICAO24, INTERVAL_SECONDS, SECRET_KEY_HEX
from common.hmac_signer import sign_payload
from common.http_client import post_telemetry
from common.telemetry_generator import TelemetryGenerator


def main() -> None:
    if FLIGHT_PLAN_ID.int == 0:
        raise ValueError("Set SIMULATOR2_FLIGHT_PLAN_ID to a flight plan created through the API.")

    generator = TelemetryGenerator(FLIGHT_PLAN_ID, ICAO24, CALLSIGN)
    print(f"Sending legitimate telemetry for {CALLSIGN} to {API_BASE_URL}/telemetry")

    while True:
        payload = generator.next_payload()
        payload["Signature"] = sign_payload(payload, SECRET_KEY_HEX)
        response = post_telemetry(API_BASE_URL, payload)
        print(f"sequence=[{payload['Sequence']}] status=[ACCEPTED]{response.status_code} body={response.text}")
        time.sleep(INTERVAL_SECONDS)


if __name__ == "__main__":
    main()
