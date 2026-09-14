import { Filter } from "lucide-react";
import { useEffect, useMemo, useState } from "react";
import { telemetryApi } from "../services/api";
import type { ApiLoadState, TelemetryLog } from "../types";
import { mockTelemetryLogs } from "../data/mockData";
import { DataTable } from "../components/ui/DataTable";
import { StatusPill } from "../components/ui/StatusPill";
import { formatDateTime } from "../utils/format";
import { toneFromStatus } from "../utils/status";

type ApiRecord = Record<string, unknown>;

function isRecord(value: unknown): value is ApiRecord {
  return typeof value === "object" && value !== null && !Array.isArray(value);
}

function readString(record: ApiRecord, keys: string[], fallback: string) {
  for (const key of keys) {
    const value = record[key];

    if (typeof value === "string" && value.trim()) {
      return value;
    }

    if (typeof value === "number") {
      return String(value);
    }
  }

  return fallback;
}

function normalizeStatus(status: string): TelemetryLog["status"] {
  const normalized = status.toLowerCase();

  if (normalized.includes("spoof") || normalized.includes("fail") || normalized.includes("reject")) {
    return "failed";
  }

  if (normalized.includes("pending")) {
    return "pending";
  }

  if (normalized.includes("delay")) {
    return "delayed";
  }

  return "success";
}

function toTelemetryLog(record: unknown, index: number): TelemetryLog {
  if (!isRecord(record)) {
    return {
      id: `unknown-${index}`,
      timestamp: new Date().toISOString(),
      aircraftId: `AIR${index + 100}`,
      signalType: "ADS-B Telemetry",
      status: "pending",
      dataSize: "12 KB",
      source: "Unknown",
      searchText: `AIR${index + 100} ADS-B Telemetry pending Unknown`,
    };
  }

  const messageId = readString(record, ["messageId", "MessageId"], `message-${index}`);
  const callsign = readString(record, ["callsign", "Callsign"], `AIR${index + 100}`);
  const icao24 = readString(record, ["icao24", "ICAO24", "iCAO24"], "Unknown");
  const status = normalizeStatus(readString(record, ["status", "Status"], "success"));
  const timestamp = readString(
    record,
    ["timestampUtc", "TimestampUtc", "timestampUTC", "TimestampUTC", "timestamp"],
    new Date().toISOString(),
  );
  const signature = readString(record, ["signature", "Signature"], "");
  const dataSize = signature
    ? `${Math.max(8, Math.round(signature.length / 2)).toFixed(0)} KB`
    : `${12 + (index % 9)} KB`;

  return {
    id: messageId,
    timestamp,
    aircraftId: callsign,
    signalType: "ADS-B Telemetry",
    status,
    dataSize,
    source: icao24,
    searchText: `${callsign} ${icao24} ADS-B Telemetry ${status} ${dataSize}`,
  };
}

export function TelemetryLogs() {
  const [logs, setLogs] = useState<TelemetryLog[]>(mockTelemetryLogs);
  const [loadState, setLoadState] = useState<ApiLoadState>("idle");
  const [statusFilter, setStatusFilter] = useState("all");
  const [aircraftFilter, setAircraftFilter] = useState("all");
  const [fromDate, setFromDate] = useState("");
  const [toDate, setToDate] = useState("");

  useEffect(() => {
    let active = true;
    setLoadState("loading");

    telemetryApi
      .getAll()
      .then((data) => {
        if (active) {
          setLogs(data.map(toTelemetryLog));
          setLoadState("ready");
        }
      })
      .catch(() => {
        if (active) {
          setLogs(mockTelemetryLogs);
          setLoadState("mock");
        }
      });

    return () => {
      active = false;
    };
  }, []);

  const aircraftOptions = useMemo(
    () => Array.from(new Set(logs.map((log) => log.aircraftId))).sort(),
    [logs],
  );

  const filteredLogs = useMemo(() => {
    return logs.filter((log) => {
      const statusMatches = statusFilter === "all" || log.status === statusFilter;
      const aircraftMatches =
        aircraftFilter === "all" || log.aircraftId === aircraftFilter;
      const date = new Date(log.timestamp);
      const afterFrom =
        !fromDate || Number.isNaN(date.getTime()) || date >= new Date(fromDate);
      const beforeTo =
        !toDate || Number.isNaN(date.getTime()) || date <= new Date(`${toDate}T23:59:59`);

      return statusMatches && aircraftMatches && afterFrom && beforeTo;
    });
  }, [aircraftFilter, fromDate, logs, statusFilter, toDate]);

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-normal text-brand-black">
            Telemetry Logs
          </h2>
          <p className="mt-1 text-sm text-zinc-500">
            Received aircraft telemetry frames with verification state
          </p>
        </div>
        <StatusPill
          label={loadState === "ready" ? "Live API" : loadState === "loading" ? "Loading" : "Sample data"}
          tone={loadState === "ready" ? "success" : loadState === "loading" ? "neutral" : "gold"}
        />
      </div>

      <section className="app-card p-5">
        <div className="mb-4 flex items-center gap-2">
          <Filter className="h-5 w-5 text-brand-gold" />
          <h3 className="text-base font-semibold text-brand-black">Filters</h3>
        </div>
        <div className="grid gap-3 md:grid-cols-4">
          <label className="space-y-2">
            <span className="text-sm font-semibold text-brand-black">Aircraft</span>
            <select
              className="field"
              value={aircraftFilter}
              onChange={(event) => setAircraftFilter(event.target.value)}
            >
              <option value="all">All aircraft</option>
              {aircraftOptions.map((aircraft) => (
                <option key={aircraft} value={aircraft}>
                  {aircraft}
                </option>
              ))}
            </select>
          </label>
          <label className="space-y-2">
            <span className="text-sm font-semibold text-brand-black">Status</span>
            <select
              className="field"
              value={statusFilter}
              onChange={(event) => setStatusFilter(event.target.value)}
            >
              <option value="all">All statuses</option>
              <option value="success">Success</option>
              <option value="failed">Failed</option>
              <option value="delayed">Delayed</option>
              <option value="pending">Pending</option>
            </select>
          </label>
          <label className="space-y-2">
            <span className="text-sm font-semibold text-brand-black">From</span>
            <input
              className="field"
              type="date"
              value={fromDate}
              onChange={(event) => setFromDate(event.target.value)}
            />
          </label>
          <label className="space-y-2">
            <span className="text-sm font-semibold text-brand-black">To</span>
            <input
              className="field"
              type="date"
              value={toDate}
              onChange={(event) => setToDate(event.target.value)}
            />
          </label>
        </div>
      </section>

      <DataTable
        title="Received Telemetry"
        rows={filteredLogs}
        searchPlaceholder="Search aircraft, signal type, source, status"
        columns={[
          {
            key: "timestamp",
            header: "Timestamp",
            sortable: true,
            sortValue: (row) => row.timestamp,
            accessor: (row) => formatDateTime(row.timestamp),
          },
          {
            key: "aircraft",
            header: "Aircraft ID",
            sortable: true,
            sortValue: (row) => row.aircraftId,
            accessor: (row) => (
              <span className="font-semibold text-brand-black">{row.aircraftId}</span>
            ),
          },
          {
            key: "signalType",
            header: "Signal Type",
            sortable: true,
            sortValue: (row) => row.signalType,
            accessor: (row) => row.signalType,
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
            key: "dataSize",
            header: "Data Size",
            sortable: true,
            sortValue: (row) => row.dataSize,
            accessor: (row) => row.dataSize,
          },
          {
            key: "source",
            header: "Source",
            sortable: true,
            sortValue: (row) => row.source,
            accessor: (row) => row.source,
          },
        ]}
      />
    </div>
  );
}
