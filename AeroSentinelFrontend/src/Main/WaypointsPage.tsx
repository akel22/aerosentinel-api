import { useEffect, useMemo, useState } from "react";
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
import PageSwitchBar from "../components/ui/button/PageSwitchBar";
import { API_BASE_URL } from "../services/api";

interface Waypoint {
  waypointId: string;
  latitude: number;
  longitude: number;
}

type SortKey = keyof Waypoint;

const WaypointsTable = () => {
  const [waypoints, setWaypoints] = useState<Waypoint[]>([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [sortKey, setSortKey] = useState<SortKey>("waypointId");
  const [sortDirection, setSortDirection] = useState<"asc" | "desc">("asc");
  const [copiedId, setCopiedId] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Pagination State (Configured to 10 items per page)
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;

  useEffect(() => {
    const fetchWaypoints = async () => {
      try {
        setLoading(true);
        setError(null);

        const response = await fetch(`${API_BASE_URL}/waypoints`, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
        });

        if (!response.ok) {
          throw new Error(`API error: ${response.status}`);
        }

        const data: Waypoint[] = await response.json();
        setWaypoints(data);
      } catch (error) {
        const errorMessage =
          error instanceof Error ? error.message : String(error);
        setError(errorMessage);
      } finally {
        setLoading(false);
      }
    };

    fetchWaypoints();
  }, []);

  const handleSort = (key: SortKey) => {
    if (sortKey === key) {
      setSortDirection((current) => (current === "asc" ? "desc" : "asc"));
    } else {
      setSortKey(key);
      setSortDirection("asc");
    }
  };

 // Filter and sort data safely
  const processedData = useMemo(() => {
    const filtered = waypoints.filter((wp) =>
    (wp?.waypointId ?? "").toLowerCase().includes(searchQuery.toLowerCase())
    );

    return [...filtered].sort((a, b) => {
      const valueA = a[sortKey] ?? "";
      const valueB = b[sortKey] ?? "";

      if (valueA < valueB) {
        return sortDirection === "asc" ? -1 : 1;
      }

      if (valueA > valueB) {
        return sortDirection === "asc" ? 1 : -1;
      }

      return 0;
    });
  }, [waypoints, searchQuery, sortKey, sortDirection]);

  // Total pages calculation
  const totalPages = Math.max(1, Math.ceil(processedData.length / itemsPerPage));

  // Current page records slice
  const paginatedData = useMemo(() => {
    const startIndex = (currentPage - 1) * itemsPerPage;
    return processedData.slice(startIndex, startIndex + itemsPerPage);
  }, [processedData, currentPage, itemsPerPage]);

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(e.target.value);
    setCurrentPage(1);
  };

  const copyCoordinates = async (wp: Waypoint) => {
    try {
      await navigator.clipboard.writeText(`${wp.latitude}, ${wp.longitude}`);
      setCopiedId(wp.waypointId);
      setTimeout(() => setCopiedId(null), 2000);
    } catch (error) {
      console.error("Failed to copy coordinates:", error);
    }
  };

  const openInMaps = (lat: number, lng: number) => {
    window.open(
      `https://www.google.com/maps/search/?api=1&query=${lat},${lng}`,
      "_blank"
    );
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
            onChange={handleSearchChange}
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
            <circle cx="11" cy="11" r="8" />
            <line x1="21" y1="21" x2="16.65" y2="16.65" />
          </svg>
        </div>

        <div className="text-sm text-gray-500 dark:text-gray-400">
          Showing {processedData.length} waypoints
        </div>
      </div>

      {/* Table */}
      <div className="overflow-hidden rounded-xl border border-gray-200 bg-white dark:border-white/[0.05] dark:bg-white/[0.03]">
        <div className="max-w-full overflow-x-auto">
          <Table>
            <TableHeader className="border-b border-gray-100 dark:border-white/[0.05]">
              <TableRow>
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
                    <button
                      onClick={() => handleSort(col.key as SortKey)}
                      className="flex items-center gap-1 transition-colors hover:text-gray-800 dark:hover:text-white"
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
              {loading ? (
                <TableRow>
                  <td
                    colSpan={4}
                    className="px-5 py-8 text-center text-gray-500 dark:text-gray-400"
                  >
                    Loading waypoints...
                  </td>
                </TableRow>
              ) : error ? (
                <TableRow>
                  <td
                    colSpan={4}
                    className="px-5 py-8 text-center text-red-500"
                  >
                    Failed to load waypoints: {error}
                  </td>
                </TableRow>
              ) : paginatedData.length > 0 ? (
                paginatedData.map((waypoint) => (
                  <TableRow key={waypoint.waypointId}>
                    <TableCell className="px-5 py-4 font-medium text-gray-800 text-start text-theme-sm dark:text-white/90">
                      {waypoint.waypointId}
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
                          className="text-gray-400 transition-colors hover:text-brand-500"
                          title="Copy Coordinates"
                        >
                          {copiedId === waypoint.waypointId ? (
                            <span className="text-xs font-medium text-green-500">
                              Copied!
                            </span>
                          ) : (
                            <svg
                              width="18"
                              height="18"
                              viewBox="0 0 24 24"
                              fill="none"
                              stroke="currentColor"
                              strokeWidth="2"
                              strokeLinecap="round"
                              strokeLinejoin="round"
                            >
                              <rect
                                x="9"
                                y="9"
                                width="13"
                                height="13"
                                rx="2"
                              />
                              <path d="M5 15H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h9a2 2 0 0 1 2 2v1" />
                            </svg>
                          )}
                        </button>

                        <button
                          onClick={() =>
                            openInMaps(waypoint.latitude, waypoint.longitude)
                          }
                          className="text-gray-400 transition-colors hover:text-brand-500"
                          title="Open in Maps"
                        >
                          <svg
                            width="18"
                            height="18"
                            viewBox="0 0 24 24"
                            fill="none"
                            stroke="currentColor"
                            strokeWidth="2"
                            strokeLinecap="round"
                            strokeLinejoin="round"
                          >
                            <path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z" />
                            <circle cx="12" cy="10" r="3" />
                          </svg>
                        </button>
                      </div>
                    </TableCell>
                  </TableRow>
                ))
              ) : (
                <TableRow>
                  <td
                    colSpan={4}
                    className="px-5 py-8 text-center text-gray-500 dark:text-gray-400"
                  >
                    No waypoints found matching "{searchQuery}"
                  </td>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </div>
      </div>

      {/* Pagination Bar */}
      {processedData.length > 0 && (
        <PageSwitchBar
          currentPage={currentPage}
          totalPages={totalPages}
          onPageChange={(page) => setCurrentPage(page)}
        />
      )}
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