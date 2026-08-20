import { useEffect, useRef, useState } from 'react'
import { AircraftStatusTable } from '../components/dashboard/AircraftStatusTable'
import { FailedTelemetryTable } from '../components/dashboard/FailedTelemetryTable'
import { MetricCard } from '../components/dashboard/MetricCard'
import { SecurityEvents } from '../components/dashboard/SecurityEvents'
import { TelemetryChart } from '../components/dashboard/TelemetryChart'
import type { DashboardSnapshot } from '../data/liveDashboardData'
import { fetchDashboardData } from '../data/liveDashboardData'

const metricLabels = [
  ['Registered aircraft', 'registeredAircraft'],
  ['Telemetry frames', 'telemetryFrames'],
  ['Authentication rate', 'authenticationRate'],
  ['Failed frames', 'failedFrames'],
] as const

function PhilippineClock() {
  const clockRef = useRef<HTMLSpanElement>(null)

  useEffect(() => {
    const updateClock = () => {
      if (clockRef.current) {
        clockRef.current.textContent = new Date().toLocaleString('en-PH', {
          timeZone: 'Asia/Manila',
          dateStyle: 'medium',
          timeStyle: 'medium',
        })
      }
    }

    updateClock()
    const timer = window.setInterval(updateClock, 1000)

    return () => window.clearInterval(timer)
  }, [])

  return <span ref={clockRef}>Loading time...</span>
}

export function DashboardPage() {
  const [snapshot, setSnapshot] = useState<DashboardSnapshot | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let isMounted = true

    fetchDashboardData()
      .then((data) => {
        if (isMounted) {
          setSnapshot(data)
          setLoading(false)
        }
      })
      .catch((fetchError) => {
        if (isMounted) {
          setError(fetchError instanceof Error ? fetchError.message : 'Unable to load dashboard state.')
          setLoading(false)
        }
      })

    return () => {
      isMounted = false
    }
  }, [])

  if (loading) {
    return <div className="rounded-sm border border-slate-700 bg-[#0c1624] p-8 text-slate-300">Loading live dashboard data...</div>
  }

  if (error || !snapshot) {
    return <div className="rounded-sm border border-rose-500/30 bg-rose-500/10 p-8 text-rose-200">{error ?? 'Dashboard snapshot unavailable.'}</div>
  }

  const metrics = metricLabels.map(([label, key]) => {
    const value = snapshot.summary[key as keyof typeof snapshot.summary]

    const formattedValue =
      key === 'authenticationRate'
        ? `${Number(value).toFixed(2)}%`
        : key === 'registeredAircraft' || key === 'telemetryFrames' || key === 'failedFrames'
          ? new Intl.NumberFormat().format(Number(value))
          : `${Number(value)}`

    const descriptionMap = {
      registeredAircraft: 'Aircraft currently in profile registry',
      telemetryFrames: 'Latest telemetry records in SQLite',
      authenticationRate: 'Verified vs. failed telemetry rate',
      failedFrames: 'Frames requiring operator review',
    }

    return {
      label,
      value: formattedValue,
      description: descriptionMap[key as keyof typeof descriptionMap],
    }
  })

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-2 border-b border-slate-700 pb-4 md:flex-row md:items-end md:justify-between">
        <div>
          <p className="text-[10px] uppercase tracking-[0.2em] text-slate-500">Live snapshot</p>
          <h2 className="mt-1 text-xl font-semibold text-slate-50">Operations Dashboard</h2>
        </div>
        <div className="text-[10px] uppercase tracking-[0.18em] text-slate-500">
          <div>Philippine Time (UTC+08:00)</div>
          <div className="mt-1 text-slate-300">
            <PhilippineClock />
          </div>
          <div className="mt-1">Snapshot generated {new Date(snapshot.generatedAt).toLocaleString()}</div>
        </div>
      </div>

      <section className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
        {metrics.map((metric) => (
          <MetricCard
            key={metric.label}
            label={metric.label}
            value={metric.value}
            description={metric.description}
          />
        ))}
      </section>

      <section className="space-y-6">
        <TelemetryChart points={snapshot.telemetryTrend} />

        <div className="grid gap-6 xl:grid-cols-[1.4fr_0.9fr]">
          <AircraftStatusTable rows={snapshot.aircraft} />
          <SecurityEvents events={snapshot.securityEvents} />
        </div>

        <FailedTelemetryTable rows={snapshot.failedTelemetryLog} />
      </section>
    </div>
  )
}
