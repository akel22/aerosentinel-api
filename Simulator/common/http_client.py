"""HTTP transport for telemetry requests."""

from __future__ import annotations

from typing import Any

import requests


def post_telemetry(api_base_url: str, payload: dict[str, Any], timeout_seconds: float = 10) -> requests.Response:
    response = requests.post(
        f"{api_base_url}/telemetry",
        json=payload,
        headers={"Accept": "application/json"},
        timeout=timeout_seconds,
    )
    return response
