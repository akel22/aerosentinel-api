import type { SecurityEvent } from '../../data/liveDashboardData'

type SecurityEventsProps = {
  events: SecurityEvent[]
}

const severityStyles: Record<string, string> = {
  CRITICAL: 'border-rose-500/30 bg-rose-500/10 text-rose-200',
  WARNING: 'border-amber-500/30 bg-amber-500/10 text-amber-200',
  INFO: 'border-cyan-500/30 bg-cyan-500/10 text-cyan-200',
}

export function SecurityEvents({ events }: SecurityEventsProps) {
  return (
    <div className="rounded-sm border border-slate-700 bg-[#0c1624] p-4">
      <div className="mb-4 flex items-center justify-between gap-3">
        <div>
          <p className="text-[10px] font-medium uppercase tracking-[0.2em] text-slate-400">
            Security Events
          </p>
          <h3 className="mt-1 text-lg font-semibold text-slate-100">Recent</h3>
        </div>
      </div>

      <div className="space-y-3">
        {events.map((event) => (
          <div key={event.id} className="rounded-sm border border-slate-800 bg-[#09131f] p-3">
            <div className="mb-2 flex items-center justify-between gap-2">
              <span className={`inline-flex items-center rounded-sm border px-2 py-0.5 text-[9px] font-semibold uppercase tracking-[0.16em] ${severityStyles[event.severity] ?? severityStyles.INFO}`}>
                {event.severity}
              </span>
              <span className="text-[10px] uppercase tracking-[0.16em] text-slate-500">{event.timestamp}</span>
            </div>
            <div className="text-sm font-medium text-slate-100">{event.type}</div>
            <div className="mt-1 text-xs text-slate-400">{event.aircraft}</div>
          </div>
        ))}
      </div>
    </div>
  )
}
