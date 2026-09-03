import { LatestAircraftStatus } from "../../services/api";
import Badge from "../ui/badge/Badge";

interface AircraftStatusProps {
  aircraftStatus?: LatestAircraftStatus[];
}

export default function AircraftStatusOverview({ aircraftStatus = [] }: AircraftStatusProps) {
  return (
    <div className="rounded-2xl border border-gray-200 bg-white px-5 pb-5 pt-5 dark:border-gray-800 dark:bg-white/[0.03] sm:px-6 sm:pt-6">
      <div className="flex flex-col gap-5 mb-6 sm:flex-row sm:justify-between">
        <div className="w-full">
          <h3 className="text-lg font-semibold text-gray-800 dark:text-white/90">
            Aircraft Status Overview
          </h3>
          <p className="mt-1 text-gray-500 text-theme-sm dark:text-gray-400">
            Real-time telemetry data for all active aircraft
          </p>
        </div>
      </div>

      <div className="max-w-full overflow-x-auto">
        <table className="w-full">
          <thead>
            <tr className="border-b border-gray-100 dark:border-gray-800">
              <th className="text-left py-3 px-4 font-medium text-gray-500 text-theme-xs dark:text-gray-400">Callsign</th>
              <th className="text-left py-3 px-4 font-medium text-gray-500 text-theme-xs dark:text-gray-400">ICAO24</th>
              <th className="text-left py-3 px-4 font-medium text-gray-500 text-theme-xs dark:text-gray-400">Status</th>
              <th className="text-left py-3 px-4 font-medium text-gray-500 text-theme-xs dark:text-gray-400">Altitude</th>
              <th className="text-left py-3 px-4 font-medium text-gray-500 text-theme-xs dark:text-gray-400">Ground Speed</th>
              <th className="text-left py-3 px-4 font-medium text-gray-500 text-theme-xs dark:text-gray-400">Last Update</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-100 dark:divide-gray-800">
            {aircraftStatus.length > 0 ? (
              aircraftStatus.map((aircraft) => (
                <tr key={aircraft.icao24}>
                  <td className="py-3 px-4 text-theme-sm text-gray-800 dark:text-white/90 font-medium">{aircraft.callsign}</td>
                  <td className="py-3 px-4 text-theme-sm text-gray-500 dark:text-gray-400">{aircraft.icao24}</td>
                  <td className="py-3 px-4">
                    <Badge color={aircraft.status === "Verified" ? "success" : "error"} size="sm">
                      {aircraft.status}
                    </Badge>
                  </td>
                  <td className="py-3 px-4 text-theme-sm text-gray-500 dark:text-gray-400">{aircraft.altitude}</td>
                  <td className="py-3 px-4 text-theme-sm text-gray-500 dark:text-gray-400">{aircraft.speed}</td>
                  <td className="py-3 px-4 text-theme-sm text-gray-500 dark:text-gray-400">{aircraft.lastTelemetry}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={6} className="py-8 px-4 text-center text-gray-500 dark:text-gray-400">
                  No aircraft data available
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
