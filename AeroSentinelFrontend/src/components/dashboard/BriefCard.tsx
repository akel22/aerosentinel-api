import { BoxIconLine, GroupIcon } from "../../icons";
import { DashboardSummary } from "../../services/api";

interface BriefCardProps {
  dashboardData?: DashboardSummary;
}

export default function BriefCard({ dashboardData }: BriefCardProps) {
  // Default values if no data is provided
  const aircraftCount = dashboardData?.aircraftProfilesCount || 0;
  const verifiedTelemetry = dashboardData?.verifiedTelemetry || 0;
  const failedTelemetry = dashboardData?.failedTelemetry || 0;
  const activeAircraft = dashboardData?.activeAircraft || 0;

  return (
    <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:gap-6">
      {/* Aircraft Profiles Card */}
      <div className="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] md:p-6">
        <div className="flex items-center justify-center w-12 h-12 bg-gray-100 rounded-xl dark:bg-gray-800">
          <GroupIcon className="text-gray-800 size-6 dark:text-white/90" />
        </div>

        <div className="flex items-end justify-between mt-5">
          <div>
            <span className="text-sm text-gray-500 dark:text-gray-400">
              Aircraft Profiles
            </span>
            <h4 className="mt-2 font-bold text-gray-800 text-title-sm dark:text-white/90">
              {aircraftCount}
            </h4>
          </div>
        </div>
      </div>
      {/* Aircraft Profiles Card End */}

      {/* Verified Telemetry Card */}
      <div className="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] md:p-6">
        <div className="flex items-center justify-center w-12 h-12 bg-gray-100 rounded-xl dark:bg-gray-800">
          <BoxIconLine className="text-gray-800 size-6 dark:text-white/90" />
        </div>
        <div className="flex items-end justify-between mt-5">
          <div>
            <span className="text-sm text-gray-500 dark:text-gray-400">
              Verified Telemetry
            </span>
            <h4 className="mt-2 font-bold text-gray-800 text-title-sm dark:text-white/90">
              {verifiedTelemetry}
            </h4>
          </div>
        </div>
      </div>
      {/* Verified Telemetry Card End */}

      {/* Failed Telemetry Card */}
      <div className="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] md:p-6">
        <div className="flex items-center justify-center w-12 h-12 bg-gray-100 rounded-xl dark:bg-gray-800">
          <BoxIconLine className="text-gray-800 size-6 dark:text-white/90" />
        </div>
        <div className="flex items-end justify-between mt-5">
          <div>
            <span className="text-sm text-gray-500 dark:text-gray-400">
              Failed Telemetry
            </span>
            <h4 className="mt-2 font-bold text-gray-800 text-title-sm dark:text-white/90">
              {failedTelemetry}
            </h4>
          </div>

        </div>
      </div>
      {/* Failed Telemetry Card End */}

      {/* Active Aircraft Card */}
      <div className="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] md:p-6">
        <div className="flex items-center justify-center w-12 h-12 bg-gray-100 rounded-xl dark:bg-gray-800">
          <GroupIcon className="text-gray-800 size-6 dark:text-white/90" />
        </div>
        <div className="flex items-end justify-between mt-5">
          <div>
            <span className="text-sm text-gray-500 dark:text-gray-400">
              Active Aircraft
            </span>
            <h4 className="mt-2 font-bold text-gray-800 text-title-sm dark:text-white/90">
              {activeAircraft}
            </h4>
          </div>
        </div>
      </div>
      {/* Active Aircraft Card End */}
    </div>
  );
}
