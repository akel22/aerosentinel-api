export type DashboardSummary = {
  registeredAircraft: number
  telemetryFrames: number
  verifiedFrames: number
  failedFrames: number
  credentialedAircraft: number
  authenticationRate: number
  activeAircraft: number
}

export type TelemetryPoint = {
  time: string
  volume: number
}

export type AircraftStatusRow = {
  callsign: string
  icao24: string
  status: string
  lastTelemetry: string
  authentication: string
  altitude: string
  speed: string
}

export type SecurityEvent = {
  id: number
  type: string
  aircraft: string
  timestamp: string
  severity: 'CRITICAL' | 'WARNING' | 'INFO'
}

export type FailedTelemetryRow = {
  id: string
  callsign: string
  aircraft: string
  timestamp: string
  category: string
  status: string
  failureReason: string
}

export type DashboardSnapshot = {
  generatedAt: string
  summary: DashboardSummary
  telemetryTrend: TelemetryPoint[]
  aircraft: AircraftStatusRow[]
  securityEvents: SecurityEvent[]
  failedTelemetryLog: FailedTelemetryRow[]
  latestTelemetry: Array<{
    messageId: string
    callsign: string
    icao24: string
    status: string
    timestampUtc: string
    sequenceNumber: number
    failureReason: string
  }>
}

export async function fetchDashboardData(): Promise<DashboardSnapshot> {
  const response = await fetch('/dashboard')

  if (!response.ok) {
    throw new Error('Unable to load dashboard snapshot from the API.')
  }

  return response.json() as Promise<DashboardSnapshot>
}
