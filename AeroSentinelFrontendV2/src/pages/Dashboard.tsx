import {
  Activity,
  AlertTriangle,
  CheckCircle2,
  Plane,
} from "lucide-react";
import { useEffect, useMemo, useState } from "react";
import {
  Area,
  AreaChart,
  CartesianGrid,
  Cell,
  Pie,
  PieChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { activityFeed, mapMarkers, mockDashboardSnapshot } from "../data/mockData";
import { dashboardApi } from "../services/api";
import type { ActivityItem, ApiLoadState, DashboardSnapshot } from "../types";
import { compactNumber, formatDateTime, formatPercent } from "../utils/format";
import { toneFromStatus } from "../utils/status";
import { ActivityLogList } from "../components/ui/ActivityLogList";
import { ChartCard } from "../components/ui/ChartCard";
import { DataTable } from "../components/ui/DataTable";
import { MapWidget } from "../components/ui/MapWidget";
import { StatCard } from "../components/ui/StatCard";
import { StatusGauge } from "../components/ui/StatusGauge";
import { StatusPill } from "../components/ui/StatusPill";

export function Dashboard() {
  const [snapshot, setSnapshot] = useState<DashboardSnapshot>(
    mockDashboardSnapshot,
  );
  const [loadState, setLoadState] = useState<ApiLoadState>("idle");

  useEffect(() => {
    let active = true;
    setLoadState("loading");

    dashboardApi
      .getSnapshot()
      .then((data) => {
        if (active) {
          setSnapshot(data);
          setLoadState("ready");
        }
      })
      .catch(() => {
        if (active) {
          setSnapshot(mockDashboardSnapshot);
          setLoadState("mock");
        }
      });

    return () => {
      active = false;
    };
  }, []);

  const kpis = [
    {
      label: "Active Flights",
      value: String(snapshot.summary.activeAircraft),
      supportingText: `${snapshot.summary.aircraftProfilesCount} aircraft profiles`,
      trend: "+8%",
      icon: <Plane className="h-5 w-5" />,
    },
    {
      label: "Aircraft in Air",
      value: String(snapshot.aircraftStatus.length),
      supportingText: "Latest tracked fleet state",
      trend: "+3",
      icon: <Activity className="h-5 w-5" />,
    },
    {
      label: "Alerts Today",
      value: String(snapshot.summary.failedTelemetry),
      supportingText: "Rejected or delayed signals",
      trend: "-12%",
      icon: <AlertTriangle className="h-5 w-5" />,
    },
    {
      label: "Fleet Health",
      value: formatPercent(snapshot.summary.verificationPercentage),
      supportingText: `${compactNumber(snapshot.summary.verifiedTelemetry)} verified frames`,
      trend: "+2%",
      icon: <CheckCircle2 className="h-5 w-5" />,
    },
  ];

  const fleetChartData = useMemo(() => {
    const pending = Math.max(
      snapshot.summary.totalTelemetry -
        snapshot.summary.verifiedTelemetry -
        snapshot.summary.failedTelemetry,
      0,
    );

    return [
      { name: "Verified", value: snapshot.summary.verifiedTelemetry, fill: "#DEC154" },
      { name: "Failed", value: snapshot.summary.failedTelemetry, fill: "#B94A48" },
      { name: "Pending", value: pending, fill: "#A1A1AA" },
    ];
  }, [snapshot.summary]);

  const eventItems: ActivityItem[] = useMemo(
    () =>
      snapshot.securityEvents.length
        ? snapshot.securityEvents.map((event) => ({
            id: String(event.id),
            title: event.event,
            subtitle: event.aircraft,
            timestamp: event.timestamp,
            tag: event.severity.toUpperCase(),
            tone: toneFromStatus(event.severity),
          }))
        : activityFeed,
    [snapshot.securityEvents],
  );

  const latestTelemetryRows = snapshot.latestTelemetry.map((item) => ({
    id: item.messageId,
    aircraft: item.callsign,
    timestamp: item.timestamp,
    status: item.status,
    sequence: item.sequenceNumber,
    searchText: `${item.callsign} ${item.icao24} ${item.status} ${item.sequenceNumber}`,
  }));

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <p className="text-sm font-medium text-zinc-500">
            Snapshot {formatDateTime(snapshot.timestamp)}
          </p>
          <h2 className="mt-1 text-3xl font-bold tracking-normal text-brand-black">
            Aviation Operations Overview
          </h2>
        </div>
        <StatusPill
          label={loadState === "ready" ? "Live API" : loadState === "loading" ? "Loading" : "Sample data"}
          tone={loadState === "ready" ? "success" : loadState === "loading" ? "neutral" : "gold"}
        />
      </div>

      <section className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
        {kpis.map((kpi) => (
          <StatCard key={kpi.label} {...kpi} />
        ))}
      </section>

      <section className="grid gap-6 xl:grid-cols-[2fr_1fr]">
        <ChartCard
          title="Flight Activity"
          subtitle="Telemetry volume across the current operating day"
        >
          <div className="h-72">
            <ResponsiveContainer width="100%" height="100%">
              <AreaChart data={snapshot.telemetryTrend}>
                <CartesianGrid stroke="#E4E4E7" vertical={false} />
                <XAxis dataKey="hour" tickLine={false} axisLine={false} />
                <YAxis tickLine={false} axisLine={false} />
                <Tooltip />
                <Area
                  type="monotone"
                  dataKey="count"
                  stroke="#181818"
                  fill="#DEC154"
                  fillOpacity={0.35}
                  strokeWidth={3}
                />
              </AreaChart>
            </ResponsiveContainer>
          </div>
        </ChartCard>

        <ChartCard title="Verification Breakdown" subtitle="Frame validation mix">
          <div className="h-72">
            <ResponsiveContainer width="100%" height="100%">
              <PieChart>
                <Pie
                  data={fleetChartData}
                  dataKey="value"
                  innerRadius={58}
                  outerRadius={92}
                  paddingAngle={3}
                >
                  {fleetChartData.map((entry) => (
                    <Cell key={entry.name} fill={entry.fill} />
                  ))}
                </Pie>
                <Tooltip />
              </PieChart>
            </ResponsiveContainer>
          </div>
        </ChartCard>
      </section>

      <section className="grid gap-6 xl:grid-cols-[2fr_1fr]">
        <MapWidget markers={mapMarkers} />
        <StatusGauge
          value={snapshot.summary.verificationPercentage}
          label="Fleet Operational Status"
          description="Verified telemetry against total received frames"
        />
      </section>

      <section className="grid gap-6 xl:grid-cols-[1fr_1.4fr]">
        <ActivityLogList title="Important Logs" items={eventItems} />
        <DataTable
          title="Latest Telemetry"
          rows={latestTelemetryRows}
          searchPlaceholder="Search latest telemetry"
          columns={[
            {
              key: "aircraft",
              header: "Aircraft",
              sortable: true,
              sortValue: (row) => row.aircraft,
              accessor: (row) => (
                <span className="font-semibold text-brand-black">{row.aircraft}</span>
              ),
            },
            {
              key: "timestamp",
              header: "Timestamp",
              sortable: true,
              sortValue: (row) => row.timestamp,
              accessor: (row) => formatDateTime(row.timestamp),
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
              key: "sequence",
              header: "Sequence",
              sortable: true,
              sortValue: (row) => row.sequence,
              accessor: (row) => row.sequence,
            },
          ]}
        />
      </section>
    </div>
  );
}
