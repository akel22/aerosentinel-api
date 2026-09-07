// TypeScript DTOs matching backend response structure

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
  delayMinutes: number; 
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

// API Service
// API Service
export const API_BASE_URL = "http://localhost:5253"; // AeroSentinel API port

export const dashboardApi = {
  //can also be written as getDashboardSnapshot = async (): Promise<DashboardSnapshot>
  async getDashboardSnapshot(): Promise<DashboardSnapshot> {
    try {
      const response = await fetch(`${API_BASE_URL}/dashboard`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error(`API error: ${response.status}`);
      }

      // 1. Read the JSON from the response
      const data = await response.json();
      console.log("Dashboard data fetched successfully:", data);
      
      // 2. Return it directly! The .NET backend automatically converted 
      // your PascalCase C# properties into camelCase JSON over the wire.
      return data as DashboardSnapshot;

    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : String(error);
        console.error("Failed to fetch dashboard data from " + API_BASE_URL + "/dashboard:", errorMessage);
        console.error("Full error:", error);
      throw error;
    }
  },
};