import { AircraftStatusTable } from '../components/dashboard/AircraftStatusTable'
import { MetricCard } from '../components/dashboard/MetricCard'
import { SecurityEvents } from '../components/dashboard/SecurityEvents'
import { TelemetryChart } from '../components/dashboard/TelemetryChart'
import { dashboardMetrics } from '../data/mockDashboardData'

export function DashboardPage() {
  return (
    <div className="space-y-6">
      <section className="grid gap-4 md:grid-cols-2 xl:grid-cols-4">
        {dashboardMetrics.map((metric) => (
          <MetricCard
            key={metric.label}
            label={metric.label}
            value={metric.value}
            description={metric.description}
          />
        ))}
      </section>

      <section className="space-y-6">
        <TelemetryChart />

        <div className="grid gap-6 xl:grid-cols-[1.4fr_0.9fr]">
          <AircraftStatusTable />
          <SecurityEvents />
        </div>
      </section>
    </div>
  )
}
