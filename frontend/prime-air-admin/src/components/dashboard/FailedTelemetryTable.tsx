import type { FailedTelemetryRow } from '../../data/liveDashboardData'

type FailedTelemetryTableProps = {
  rows: FailedTelemetryRow[]
}

const categoryStyles: Record<string, string> = {
  Spoofed: 'border-rose-500/30 bg-rose-500/10 text-rose-200',
  Replay: 'border-amber-500/30 bg-amber-500/10 text-amber-200',
  'Invalid Credential': 'border-cyan-500/30 bg-cyan-500/10 text-cyan-200',
}

export function FailedTelemetryTable({ rows }: FailedTelemetryTableProps) {
  return (
    <div className="rounded-sm border border-slate-700 bg-[#0c1624] p-4">
      <div className="mb-4 flex items-center justify-between gap-3">
        <div>
          <p className="text-[10px] font-medium uppercase tracking-[0.2em] text-slate-400">
            Failed Telemetry Log
          </p>
          <h3 className="mt-1 text-lg font-semibold text-slate-100">Spoofed • Replay • Invalid credential</h3>
        </div>
      </div>

      {rows.length === 0 ? (
        <div className="rounded-sm border border-slate-800 bg-[#09131f] p-4 text-sm text-slate-300">
          No failed telemetry records are present in the current live SQLite snapshot.
        </div>
      ) : (
        <div className="overflow-x-auto">
          <table className="min-w-full border-separate border-spacing-y-2 text-left text-sm">
            <thead>
              <tr className="text-[10px] uppercase tracking-[0.2em] text-slate-500">
                <th className="px-3 py-2 font-medium">Aircraft</th>
                <th className="px-3 py-2 font-medium">Callsign</th>
                <th className="px-3 py-2 font-medium">Category</th>
                <th className="px-3 py-2 font-medium">Status</th>
                <th className="px-3 py-2 font-medium">Logged</th>
                <th className="px-3 py-2 font-medium">Reason</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((row) => (
                <tr key={row.id} className="bg-[#09131f] text-slate-200">
                  <td className="rounded-l-sm border border-r-0 border-slate-700 px-3 py-3 font-medium text-slate-100">
                    {row.aircraft}
                  </td>
                  <td className="border border-l-0 border-r-0 border-slate-700 px-3 py-3 text-slate-300">
                    {row.callsign}
                  </td>
                  <td className="border border-l-0 border-r-0 border-slate-700 px-3 py-3">
                    <span className={`inline-flex items-center rounded-sm border px-2 py-1 text-[10px] font-medium uppercase tracking-[0.12em] ${categoryStyles[row.category] ?? 'border-slate-600 bg-slate-800 text-slate-200'}`}>
                      {row.category}
                    </span>
                  </td>
                  <td className="border border-l-0 border-r-0 border-slate-700 px-3 py-3 text-slate-300">
                    {row.status}
                  </td>
                  <td className="border border-l-0 border-r-0 border-slate-700 px-3 py-3 text-slate-300">
                    {row.timestamp}
                  </td>
                  <td className="rounded-r-sm border border-l-0 border-slate-700 px-3 py-3 text-slate-300">
                    {row.failureReason}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  )
}
