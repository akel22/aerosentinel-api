import { useState } from "react";
import { Dropdown } from "../ui/dropdown/Dropdown";
import { DropdownItem } from "../ui/dropdown/DropdownItem";
import { MoreDotIcon } from "../../icons";
import { LatestAircraftStatus } from "../../services/api";
import Badge from "../ui/badge/Badge";

interface DemographicCardProps {
  aircraftStatus?: LatestAircraftStatus[];
}

export default function AircraftStatus({ aircraftStatus = [] }: DemographicCardProps) {
  const [isOpen, setIsOpen] = useState(false);

  function toggleDropdown() {
    setIsOpen(!isOpen);
  }

  function closeDropdown() {
    setIsOpen(false);
  }

  // Get top 5 aircraft by status
  const topAircraft = aircraftStatus.slice(0, 5);

  return (
    <div className="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] sm:p-6">
      <div className="flex justify-between">
        <div>
          <h3 className="text-lg font-semibold text-gray-800 dark:text-white/90">
            Aircraft Status
          </h3>
          <p className="mt-1 text-gray-500 text-theme-sm dark:text-gray-400">
            Latest aircraft status updates
          </p>
        </div>
        <div className="relative inline-block">
          <button className="dropdown-toggle" onClick={toggleDropdown}>
            <MoreDotIcon className="text-gray-400 hover:text-gray-700 dark:hover:text-gray-300 size-6" />
          </button>
          <Dropdown
            isOpen={isOpen}
            onClose={closeDropdown}
            className="w-40 p-2"
          >
            <DropdownItem
              onItemClick={closeDropdown}
              className="flex w-full font-normal text-left text-gray-500 rounded-lg hover:bg-gray-100 hover:text-gray-700 dark:text-gray-400 dark:hover:bg-white/5 dark:hover:text-gray-300"
            >
              View All
            </DropdownItem>
            <DropdownItem
              onItemClick={closeDropdown}
              className="flex w-full font-normal text-left text-gray-500 rounded-lg hover:bg-gray-100 hover:text-gray-700 dark:text-gray-400 dark:hover:bg-white/5 dark:hover:text-gray-300"
            >
              Refresh
            </DropdownItem>
          </Dropdown>
        </div>
      </div>

      <div className="mt-6 space-y-3">
        {topAircraft.length > 0 ? (
          topAircraft.map((aircraft) => (
            <div key={aircraft.icao24} className="flex items-center justify-between pb-3 border-b border-gray-100 dark:border-gray-800 last:border-0">
              <div className="flex-1">
                <p className="text-sm font-medium text-gray-800 dark:text-white/90">{aircraft.callsign}</p>
                <p className="text-xs text-gray-500 dark:text-gray-400">{aircraft.icao24}</p>
              </div>
              <div className="text-right">
                <Badge color={aircraft.status === "Ongoing" ? "info" : "error"}>
                  {aircraft.status}
                </Badge>
              </div>
            </div>
          ))
        ) : (
          <p className="text-sm text-gray-500 dark:text-gray-400">No aircraft data available</p>
        )}
      </div>
    </div>
  );
}
