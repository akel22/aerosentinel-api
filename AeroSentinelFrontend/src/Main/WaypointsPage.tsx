import React, { useState, useMemo } from "react";
import PageBreadcrumb from "../components/common/PageBreadCrumb";
import ComponentCard from "../components/common/ComponentCard";
import PageMeta from "../components/common/PageMeta";
import {
  Table,
  TableBody,
  TableCell,
  TableHeader,
  TableRow,
} from "../components/ui/table";

interface Waypoint {
  id: string;
  latitude: number;
  longitude: number;
}

const waypointsData: Waypoint[] = [
  { id: "ABLBG", latitude: 13.843603, longitude: 120.113717 },
  { id: "ABMBS", latitude: 14.978178, longitude: 120.488897 },
  { id: "AGVAR", latitude: 19.414106, longitude: 120.628464 },
  { id: "AKOTA", latitude: 16.462028, longitude: 117.20675 },
  { id: "ALDIN", latitude: 9.116997, longitude: 123.301075 },
];

type SortKey = keyof Waypoint;

const WaypointsTable = () => {
  const [searchQuery, setSearchQuery] = useState("");
  const [sortKey, setSortKey] = useState<SortKey>("id");
  const [sortDirection, setSortDirection] = useState<"asc" | "desc">("asc");
  const [copiedId, setCopiedId] = useState<string | null>(null);

  // Handle sorting logic
  const handleSort = (key: SortKey) => {
    if (sortKey === key) {
      setSortDirection(sortDirection === "asc" ? "desc" : "asc");
    } else {
      setSortKey(key);
      setSortDirection("asc");
    }
  };

  // Filter and sort data
  const processedData = useMemo(() => {
    let filtered = waypointsData.filter((wp) =>
      wp.id.toLowerCase().includes(searchQuery.toLowerCase())
    );

    return filtered.sort((a, b) => {
      if (a[sortKey] < b[sortKey]) return sortDirection === "asc" ? -1 : 1;
      if (a[sortKey] > b[sortKey]) return sortDirection === "asc" ? 1 : -1;
      return 0;
    });
  }, [searchQuery, sortKey, sortDirection]);

  // Utility to copy coordinates
  const copyCoordinates = (wp: Waypoint) => {
    navigator.clipboard.writeText(`${wp.latitude}, ${wp.longitude}`);
    setCopiedId(wp.id);
    setTimeout(() => setCopiedId(null), 2000);
  };

  // Utility to open in Maps
  const openInMaps = (lat: number, lng: number) => {
    window.open(`https://www.google.com/maps/search/?api=1&query=${lat},${lng}`, "_blank");
  };

  return (
    <div className="space-y-4">
      {/* Search Bar */}
      <div className="flex items-center justify-between">
        <div className="relative w-full max-w-sm">
          <input
            type="text"
            placeholder="Search Waypoint ID..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full rounded-lg border border-gray-200 bg-transparent px-4 py-2.5 text-sm text-gray-800 outline-none focus:border-brand-500 focus:ring-1 focus:ring-brand-500 dark:border-white/[0.1] dark:text-white/90 dark:focus:border-brand-500"
          />
          <svg
            className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 dark:text-white/50"
            width="18"
            height="18"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
          >
            <circle cx="11" cy="11" r="8"></circle>
            <line x1="21" y1="21" x2="16.65" y2="16.65"></line>
          </svg>
        </div>
        <div className="text-sm text-gray-500 dark:text-gray-400">
          Showing {processedData.length} waypoints
        </div>
      </div>

      <div className="overflow-hidden rounded-xl border border-gray-200 bg-white dark:border-white/[0.05] dark:bg-white/[0.03]">
        <div className="max-w-full overflow-x-auto">
         <Table>
  <TableHeader className="border-b border-gray-100 dark:border-white/[0.05]">
    <TableRow>
      {/* Sortable Headers */}
      {[
        { key: "id", label: "Waypoint ID" },
        { key: "latitude", label: "Latitude" },
        { key: "longitude", label: "Longitude" },
      ].map((col) => (
        <TableCell
          key={col.key}
          isHeader
          className="px-5 py-3 font-medium text-gray-500 text-start text-theme-xs dark:text-gray-400"
        >
          {/* Fix: Moved onClick to a button inside the Cell */}
          <button
            onClick={() => handleSort(col.key as SortKey)}
            className="flex items-center gap-1 hover:text-gray-800 dark:hover:text-white transition-colors"
          >
            {col.label}
            {sortKey === col.key && (
              <span className="text-[10px]">
                {sortDirection === "asc" ? "▲" : "▼"}
              </span>
            )}
          </button>
        </TableCell>
      ))}
      <TableCell
        isHeader
        className="px-5 py-3 font-medium text-gray-500 text-end text-theme-xs dark:text-gray-400"
      >
        Actions
      </TableCell>
    </TableRow>
  </TableHeader>

  <TableBody className="divide-y divide-gray-100 dark:divide-white/[0.05]">
    {processedData.length > 0 ? (
      processedData.map((waypoint) => (
        <TableRow key={waypoint.id}>
          <TableCell className="px-5 py-4 font-medium text-gray-800 text-start text-theme-sm dark:text-white/90">
            {waypoint.id}
          </TableCell>
          <TableCell className="px-5 py-4 text-gray-500 text-start text-theme-sm dark:text-gray-400">
            {waypoint.latitude}
          </TableCell>
          <TableCell className="px-5 py-4 text-gray-500 text-start text-theme-sm dark:text-gray-400">
            {waypoint.longitude}
          </TableCell>
          <TableCell className="px-5 py-4 text-end">
            <div className="flex items-center justify-end gap-3">
              <button
                onClick={() => copyCoordinates(waypoint)}
                className="text-gray-400 hover:text-brand-500 transition-colors"
                title="Copy Coordinates"
              >
                {copiedId === waypoint.id ? (
                  <span className="text-xs text-green-500 font-medium">Copied!</span>
                ) : (
                  <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                    <rect x="9" y="9" width="13" height="13" rx="2" ry="2"></rect>
                    <path d="M5 15H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h9a2 2 0 0 1 2 2v1"></path>
                  </svg>
                )}
              </button>
              
              <button
                onClick={() => openInMaps(waypoint.latitude, waypoint.longitude)}
                className="text-gray-400 hover:text-brand-500 transition-colors"
                title="Open in Maps"
              >
                <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                  <path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"></path>
                  <circle cx="12" cy="10" r="3"></circle>
                </svg>
              </button>
            </div>
          </TableCell>
        </TableRow>
      ))
    ) : (
      <TableRow>
        {/* Fix: Used standard HTML <td> for colSpan compatibility */}
        <td colSpan={4} className="px-5 py-8 text-center text-gray-500 dark:text-gray-400">
          No waypoints found matching "{searchQuery}"
        </td>
      </TableRow>
    )}
  </TableBody>
</Table>
        </div>
      </div>
    </div>
  );
};

export default function WaypointsPage() {
  return (
    <>
      <PageMeta
        title="Waypoints | TailAdmin Dashboard"
        description="Aeronautical waypoints and geographical coordinates tracking."
      />
      
      <PageBreadcrumb pageTitle="Waypoints" />
      
      <div className="space-y-6">
        <ComponentCard title="Aeronautical Waypoints Log">
          <WaypointsTable />
        </ComponentCard>
      </div>
    </>
  );
}