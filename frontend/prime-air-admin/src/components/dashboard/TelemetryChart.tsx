import type { TelemetryPoint } from '../../data/liveDashboardData'

type TelemetryChartProps = {
  points: TelemetryPoint[]
}

export function TelemetryChart({ points }: TelemetryChartProps) {
  const safePoints = points.length > 0 ? points : [{ time: 'N/A', volume: 0 }]
  const maxVolume = Math.max(...safePoints.map((point) => point.volume), 1)

  return (
    <div className="rounded-sm border border-slate-700 bg-[#0c1624] p-4">
      <div className="mb-5 flex items-center justify-between gap-3">
        <div>
          <p className="text-[10px] font-medium uppercase tracking-[0.2em] text-slate-400">
            Telemetry Activity
          </p>
          <h3 className="mt-1 text-lg font-semibold text-slate-100">Last 24 hours</h3>
        </div>
        <span className="rounded-sm border border-emerald-500/30 bg-emerald-500/10 px-2 py-1 text-[10px] font-medium uppercase tracking-[0.2em] text-emerald-300">
          Active
        </span>
      </div>

      <div className="flex h-48 items-end gap-3 rounded-sm border border-slate-800 bg-[#09131f] p-3">
        {safePoints.map((point) => {
          const height = `${(point.volume / maxVolume) * 100}%`

          return (
            <div key={`${point.time}-${point.volume}`} className="flex flex-1 flex-col items-center justify-end gap-2">
              <div className="flex h-full w-full items-end justify-center">
                <div
                  className="w-full max-w-10 rounded-t-sm border border-cyan-400/40 bg-cyan-500/75"
                  style={{ height }}
                  title={`${point.time}: ${point.volume}`}
                  aria-label={`${point.time}: ${point.volume}`}
                />
              </div>
              <span className="text-[10px] font-medium uppercase tracking-[0.14em] text-slate-400">
                {point.time}
              </span>
            </div>
          )
        })}
      </div>
    </div>
  )
}
