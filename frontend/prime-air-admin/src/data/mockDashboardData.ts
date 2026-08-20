export type DashboardMetric = {
  label: string
  value: string
  description: string
}

export type TelemetryPoint = {
  time: string
  volume: number
}

export type AircraftStatusRow = {
  callsign: string
  icao24: string
  status: 'Active' | 'Warning'
  lastTelemetry: string
  authentication: 'Verified' | 'Verification Failed'
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

export const dashboardMetrics: DashboardMetric[] = [
  {
    label: 'Total Aircraft',
    value: '24',
    description: 'Aircraft registered',
  },
  {
    label: 'Telemetry Received',
    value: '1.28M',
    description: 'Messages processed',
  },
  {
    label: 'Authentication Rate',
    value: '99.82%',
    description: 'Telemetry successfully authenticated',
  },
  {
    label: 'Security Events',
    value: '7',
    description: 'Requires attention',
  },
]

export const telemetryActivity: TelemetryPoint[] = [
  { time: '00:00', volume: 42 },
  { time: '04:00', volume: 68 },
  { time: '08:00', volume: 144 },
  { time: '12:00', volume: 204 },
  { time: '16:00', volume: 186 },
  { time: '20:00', volume: 110 },
]

export const aircraftStatus: AircraftStatusRow[] = [
  {
    callsign: 'PRM101',
    icao24: 'A12345',
    status: 'Active',
    lastTelemetry: '2 sec ago',
    authentication: 'Verified',
    altitude: '32,000 ft',
    speed: '450 kt',
  },
  {
    callsign: 'PRM204',
    icao24: 'A67890',
    status: 'Active',
    lastTelemetry: '4 sec ago',
    authentication: 'Verified',
    altitude: '28,000 ft',
    speed: '420 kt',
  },
  {
    callsign: 'PRM315',
    icao24: 'B12345',
    status: 'Warning',
    lastTelemetry: '18 sec ago',
    authentication: 'Verification Failed',
    altitude: '31,000 ft',
    speed: '438 kt',
  },
  {
    callsign: 'PRM422',
    icao24: 'B67890',
    status: 'Active',
    lastTelemetry: '3 sec ago',
    authentication: 'Verified',
    altitude: '35,000 ft',
    speed: '461 kt',
  },
]

export const securityEvents: SecurityEvent[] = [
  {
    id: 1,
    type: 'Invalid HMAC Signature',
    aircraft: 'PRM315',
    timestamp: '2 minutes ago',
    severity: 'CRITICAL',
  },
  {
    id: 2,
    type: 'Replay Attempt Detected',
    aircraft: 'PRM204',
    timestamp: '8 minutes ago',
    severity: 'WARNING',
  },
  {
    id: 3,
    type: 'Telemetry Validation Failure',
    aircraft: 'PRM422',
    timestamp: '14 minutes ago',
    severity: 'INFO',
  },
  {
    id: 4,
    type: 'Unknown Aircraft Credential',
    aircraft: 'PRM101',
    timestamp: '18 minutes ago',
    severity: 'WARNING',
  },
]
