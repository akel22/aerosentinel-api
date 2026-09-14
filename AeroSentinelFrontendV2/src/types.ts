import type { ReactNode } from "react";

export type PageId =
  | "dashboard"
  | "analytics"
  | "settings"
  | "waypoints"
  | "telemetry";

export type ApiLoadState = "idle" | "loading" | "ready" | "mock";

export type StatusTone = "success" | "warning" | "danger" | "neutral" | "gold";

export interface DashboardSummary {
  aircraftProfilesCount: number;
  totalTelemetry: number;
  verifiedTelemetry: number;
  failedTelemetry: number;
  credentialsCount: number;
  verificationPercentage: number;
  activeAircraft: number;
}

export interface TelemetryTrendPoint {
  hour: string;
  count: number;
}

export interface LatestAircraftStatus {
  callsign: string;
  icao24: string;
  status: string;
  lastTelemetry: string;
  authentication: string;
  altitude: number;
  speed: number;
}

export interface SecurityEvent {
  id: number;
  event: string;
  aircraft: string;
  timestamp: string;
  severity: string;
}

export interface FailedTelemetry {
  messageId: string;
  callsign: string;
  icao24: string;
  timestamp: string;
  type: string;
  status: string;
  reason: string;
}

export interface LatestTelemetry {
  messageId: string;
  callsign: string;
  icao24: string;
  status: string;
  timestamp: string;
  sequenceNumber: number;
  failureReason: string;
}

export interface FinishedFlight {
  flightPlanId: string;
  callsign: string;
  icao24: string;
  departureAirport: string;
  destinationAirport: string;
  estimatedArrivalTimeUtc: string;
  actualArrivalTimeUtc: string;
  delayMinutes: number | string;
}

export interface DashboardSnapshot {
  timestamp: string;
  summary: DashboardSummary;
  telemetryTrend: TelemetryTrendPoint[];
  aircraftStatus: LatestAircraftStatus[];
  securityEvents: SecurityEvent[];
  failedTelemetry: FailedTelemetry[];
  latestTelemetry: LatestTelemetry[];
  finishedFlights: FinishedFlight[];
}

export interface Waypoint {
  waypointId: string;
  latitude: number;
  longitude: number;
}

export interface WaypointRow extends Waypoint {
  id: string;
  name: string;
  coordinates: string;
  region: string;
  status: "active" | "standby" | "restricted";
  lastUsed: string;
  searchText: string;
}

export interface TelemetryLog {
  id: string;
  timestamp: string;
  aircraftId: string;
  signalType: string;
  status: "success" | "failed" | "delayed" | "pending";
  dataSize: string;
  source: string;
  searchText: string;
}

export interface MapMarker {
  id: string;
  label: string;
  location: string;
  top: string;
  left: string;
  status: string;
  updatedAt: string;
}

export interface ActivityItem {
  id: string;
  title: string;
  subtitle: string;
  timestamp: string;
  tag?: string;
  tone: StatusTone;
}

export interface DataTableColumn<T> {
  key: string;
  header: string;
  accessor: (row: T) => ReactNode;
  sortable?: boolean;
  sortValue?: (row: T) => string | number;
  className?: string;
}
