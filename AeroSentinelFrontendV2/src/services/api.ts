import type { DashboardSnapshot, Waypoint } from "../types";

export const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL ?? "http://localhost:5253";

async function getJson<T>(path: string): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    headers: {
      Accept: "application/json",
    },
  });

  if (!response.ok) {
    throw new Error(`AeroSentinel API ${path} returned ${response.status}`);
  }

  return (await response.json()) as T;
}

export const dashboardApi = {
  getSnapshot() {
    return getJson<DashboardSnapshot>("/dashboard");
  },
};

export const waypointApi = {
  getAll() {
    return getJson<Waypoint[]>("/waypoints");
  },
};

export const telemetryApi = {
  getAll() {
    return getJson<unknown[]>("/telemetry");
  },
};
