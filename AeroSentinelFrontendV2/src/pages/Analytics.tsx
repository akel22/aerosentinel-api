import { useState } from "react";
import {
  Area,
  AreaChart,
  Bar,
  BarChart,
  CartesianGrid,
  Cell,
  Line,
  LineChart,
  Pie,
  PieChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import {
  analyticsLogs,
  analyticsTrend,
  fleetBreakdown,
  regionBreakdown,
  routeReliability,
} from "../data/mockData";
import { ChartCard } from "../components/ui/ChartCard";
import { DataTable } from "../components/ui/DataTable";
import { StatusPill } from "../components/ui/StatusPill";
import { formatDateTime } from "../utils/format";
import { toneFromStatus } from "../utils/status";

const ranges = ["Day", "Week", "Month", "Year"] as const;

export function Analytics() {
  const [range, setRange] = useState<(typeof ranges)[number]>("Week");

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-normal text-brand-black">
            Operational Analytics
          </h2>
          <p className="mt-1 text-sm text-zinc-500">
            Comparative flight, telemetry, route, and region performance
          </p>
        </div>
        <div className="inline-flex rounded-xl border border-zinc-200 bg-white p-1 shadow-panel">
          {ranges.map((item) => (
            <button
              key={item}
              type="button"
              className={`rounded-lg px-3 py-2 text-sm font-semibold transition ${
                item === range
                  ? "bg-brand-black text-white"
                  : "text-zinc-500 hover:bg-zinc-100 hover:text-brand-black"
              }`}
              onClick={() => setRange(item)}
            >
              {item}
            </button>
          ))}
        </div>
      </div>

      <section className="grid gap-6 xl:grid-cols-2">
        <ChartCard title={`Telemetry Integrity By ${range}`} subtitle="Verified, delayed, and failed message streams">
          <div className="h-80">
            <ResponsiveContainer width="100%" height="100%">
              <AreaChart data={analyticsTrend}>
                <CartesianGrid stroke="#E4E4E7" vertical={false} />
                <XAxis dataKey="label" tickLine={false} axisLine={false} />
                <YAxis tickLine={false} axisLine={false} />
                <Tooltip />
                <Area
                  dataKey="verified"
                  type="monotone"
                  stroke="#181818"
                  fill="#DEC154"
                  fillOpacity={0.35}
                  strokeWidth={3}
                />
                <Area
                  dataKey="delayed"
                  type="monotone"
                  stroke="#71717A"
                  fill="#D4D4D8"
                  fillOpacity={0.4}
                  strokeWidth={2}
                />
              </AreaChart>
            </ResponsiveContainer>
          </div>
        </ChartCard>

        <ChartCard title="Regional Operations" subtitle="Flights and alert count by coverage region">
          <div className="h-80">
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={regionBreakdown}>
                <CartesianGrid stroke="#E4E4E7" vertical={false} />
                <XAxis dataKey="region" tickLine={false} axisLine={false} />
                <YAxis tickLine={false} axisLine={false} />
                <Tooltip />
                <Bar dataKey="flights" fill="#181818" radius={[6, 6, 0, 0]} />
                <Bar dataKey="alerts" fill="#DEC154" radius={[6, 6, 0, 0]} />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </ChartCard>
      </section>

      <section className="grid gap-6 xl:grid-cols-[0.9fr_1.1fr]">
        <ChartCard title="Fleet Status Breakdown" subtitle="Aircraft availability by state">
          <div className="h-72">
            <ResponsiveContainer width="100%" height="100%">
              <PieChart>
                <Pie
                  data={fleetBreakdown}
                  dataKey="value"
                  outerRadius={96}
                  innerRadius={52}
                  paddingAngle={3}
                >
                  {fleetBreakdown.map((entry) => (
                    <Cell key={entry.name} fill={entry.fill} />
                  ))}
                </Pie>
                <Tooltip />
              </PieChart>
            </ResponsiveContainer>
          </div>
        </ChartCard>

        <ChartCard title="Route Reliability" subtitle="Highest-volume routes with verified message rates">
          <div className="h-72">
            <ResponsiveContainer width="100%" height="100%">
              <LineChart data={routeReliability}>
                <CartesianGrid stroke="#E4E4E7" vertical={false} />
                <XAxis dataKey="route" tickLine={false} axisLine={false} />
                <YAxis tickLine={false} axisLine={false} domain={[80, 100]} />
                <Tooltip />
                <Line
                  type="monotone"
                  dataKey="reliability"
                  stroke="#DEC154"
                  strokeWidth={3}
                  dot={{ r: 5, fill: "#181818", stroke: "#DEC154", strokeWidth: 2 }}
                />
              </LineChart>
            </ResponsiveContainer>
          </div>
        </ChartCard>
      </section>

      <DataTable
        title="Detailed Analytics Logs"
        rows={analyticsLogs}
        searchPlaceholder="Search analytics logs"
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
            header: "Aircraft",
            sortable: true,
            sortValue: (row) => row.aircraftId,
            accessor: (row) => (
              <span className="font-semibold text-brand-black">{row.aircraftId}</span>
            ),
          },
          {
            key: "signal",
            header: "Signal",
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
