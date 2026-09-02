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

export interface AircraftStatus {
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
  aircraftStatus: AircraftStatus[];
  securityEvents: SecurityEvent[];
  failedTelemetry: FailedTelemetry[];
  latestTelemetry: LatestTelemetry[];
  finishedFlights: FinishedFlight[];
}

// API Service
const API_BASE_URL = "http://localhost:5253"; // AeroSentinel API port

export const dashboardApi = {
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

      // Map the response based on your backend's actual field names
      const data = await response.json();
      
        console.log("Dashboard data fetched successfully:", data);
      
      return {
        timestamp: data.generatedAt,
        summary: {
          aircraftProfilesCount: data.summary?.registeredAircraft || 0,
          totalTelemetry: data.summary?.telemetryFrames || 0,
          verifiedTelemetry: data.summary?.verifiedFrames || 0,
          failedTelemetry: data.summary?.failedFrames || 0,
          credentialsCount: data.summary?.credentialedAircraft || 0,
          verificationPercentage: data.summary?.authenticationRate || 0,
          activeAircraft: data.summary?.activeAircraft || 0,
        },
        telemetryTrend: data.telemetryTrend || [],
        aircraftStatus: data.aircraft || [],
        securityEvents: data.securityEvents || [],
        failedTelemetry: data.failedTelemetryLog || [],
        latestTelemetry: data.latestTelemetry || [],
        finishedFlights: data.finishedFlights || [],
      };
    } catch (error) {
        const errorMessage = error instanceof Error ? error.message : String(error);
        console.error("Failed to fetch dashboard data from " + API_BASE_URL + "/dashboard:", errorMessage);
        console.error("Full error:", error);
      throw error;
    }
  },
};
