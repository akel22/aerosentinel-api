import { useEffect, useMemo, useState } from "react";
import { waypointApi } from "../services/api";
import type { ApiLoadState, Waypoint, WaypointRow } from "../types";
import { mockWaypoints } from "../data/mockData";
import { DataTable } from "../components/ui/DataTable";
import { StatusPill } from "../components/ui/StatusPill";
import { formatCoordinates } from "../utils/format";
import { toneFromStatus } from "../utils/status";

function regionFromCoordinates(latitude: number) {
  if (latitude >= 15) {
    return "Northern Luzon";
  }

  if (latitude >= 11) {
    return "Luzon";
  }

  if (latitude >= 8) {
    return "Visayas";
  }

  return "Mindanao";
}

function waypointStatus(index: number): WaypointRow["status"] {
  if (index % 7 === 0) {
    return "restricted";
  }

  if (index % 3 === 0) {
    return "standby";
  }

  return "active";
}

function toWaypointRows(waypoints: Waypoint[]): WaypointRow[] {
  return waypoints.map((waypoint, index) => {
    const status = waypointStatus(index + 1);
    const region = regionFromCoordinates(waypoint.latitude);
    const coordinates = formatCoordinates(waypoint.latitude, waypoint.longitude);

    return {
      ...waypoint,
      id: waypoint.waypointId,
      name: `${waypoint.waypointId} Fix`,
      coordinates,
      region,
      status,
      lastUsed: `2026-09-${String(14 - (index % 5)).padStart(2, "0")} ${String(
        8 - (index % 4),
      ).padStart(2, "0")}:12`,
      searchText: `${waypoint.waypointId} ${coordinates} ${region} ${status}`,
    };
  });
}

export function Waypoints() {
  const [rows, setRows] = useState<WaypointRow[]>(mockWaypoints);
  const [loadState, setLoadState] = useState<ApiLoadState>("idle");

  useEffect(() => {
    let active = true;
    setLoadState("loading");

    waypointApi
      .getAll()
      .then((data) => {
        if (active) {
          setRows(toWaypointRows(data));
          setLoadState("ready");
        }
      })
      .catch(() => {
        if (active) {
          setRows(mockWaypoints);
          setLoadState("mock");
        }
      });

    return () => {
      active = false;
    };
  }, []);

  const statusLabel = useMemo(() => {
    if (loadState === "ready") {
      return "Live API";
    }

    if (loadState === "loading") {
      return "Loading";
    }

    return "Sample data";
  }, [loadState]);

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-normal text-brand-black">
            Waypoint Registry
          </h2>
          <p className="mt-1 text-sm text-zinc-500">
            Searchable and sortable waypoint coordinate records
          </p>
        </div>
        <StatusPill
          label={statusLabel}
          tone={loadState === "ready" ? "success" : loadState === "loading" ? "neutral" : "gold"}
        />
      </div>

      <DataTable
        title="Waypoints"
        rows={rows}
        searchPlaceholder="Search waypoint ID, region, or status"
        columns={[
          {
            key: "waypointId",
            header: "Waypoint ID",
            sortable: true,
            sortValue: (row) => row.waypointId,
            accessor: (row) => (
              <span className="font-semibold text-brand-black">
                {row.waypointId}
              </span>
            ),
          },
          {
            key: "name",
            header: "Name",
            sortable: true,
            sortValue: (row) => row.name,
            accessor: (row) => row.name,
          },
          {
            key: "coordinates",
            header: "Coordinates",
            sortable: true,
            sortValue: (row) => row.latitude,
            accessor: (row) => row.coordinates,
          },
          {
            key: "region",
            header: "Region",
            sortable: true,
            sortValue: (row) => row.region,
            accessor: (row) => row.region,
          },
          {
            key: "status",
            header: "Status",
            sortable: true,
            sortValue: (row) => row.status,
            accessor: (row) => (
              <StatusPill label={row.status} tone={toneFromStatus(row.status)} />
            ),
          },
          {
            key: "lastUsed",
            header: "Last Used",
            sortable: true,
            sortValue: (row) => row.lastUsed,
            accessor: (row) => row.lastUsed,
          },
        ]}
      />
    </div>
  );
}
