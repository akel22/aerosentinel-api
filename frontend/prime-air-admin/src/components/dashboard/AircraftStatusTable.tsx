import { aircraftStatus } from '../../data/mockDashboardData'

const statusStyles = {
  Active: 'border-emerald-500/30 bg-emerald-500/10 text-emerald-200',
  Warning: 'border-amber-500/30 bg-amber-500/10 text-amber-200',
} as const

const authStyles = {
  Verified: 'border-cyan-500/30 bg-cyan-500/10 text-cyan-200',
  'Verification Failed': 'border-rose-500/30 bg-rose-500/10 text-rose-200',
} as const

export function AircraftStatusTable() {
  return (
    <div className="rounded-sm border border-slate-700 bg-[#0c1624] p-4">
      <div className="mb-4 flex items-center justify-between gap-3">
        <div>
          <p className="text-[10px] font-medium uppercase tracking-[0.2em] text-slate-400">
            Aircraft Status
          </p>
          <h3 className="mt-1 text-lg font-semibold text-slate-100">Monitoring</h3>
        </div>
      </div>

      <div className="overflow-x-auto">
        <table className="min-w-full border-separate border-spacing-y-2 text-left text-sm">
          <thead>
            <tr className="text-[10px] uppercase tracking-[0.2em] text-slate-500">
              <th className="px-3 py-2 font-medium">Callsign</th>
              <th className="px-3 py-2 font-medium">ICAO24</th>
              <th className="px-3 py-2 font-medium">Status</th>
              <th className="px-3 py-2 font-medium">Last Telemetry</th>
              <th className="px-3 py-2 font-medium">Authentication</th>
              <th className="px-3 py-2 font-medium">Altitude</th>
              <th className="px-3 py-2 font-medium">Speed</th>
            </tr>
          </thead>
          <tbody>
            {aircraftStatus.map((aircraft) => (
              <tr key={aircraft.callsign} className="bg-[#09131f] text-slate-200">
                <td className="rounded-l-sm border border-r-0 border-slate-700 px-3 py-3 font-medium text-slate-100">
                  {aircraft.callsign}
                </td>
                <td className="border border-l-0 border-r-0 border-slate-700 px-3 py-3 text-slate-300">
                  {aircraft.icao24}
                </td>
                <td className="border border-l-0 border-r-0 border-slate-700 px-3 py-3">
                  <span className={`inline-flex items-center rounded-sm border px-2 py-1 text-[10px] font-medium uppercase tracking-[0.12em] ${statusStyles[aircraft.status]}`}>
                    {aircraft.status}
                  </span>
                </td>
                <td className="border border-l-0 border-r-0 border-slate-700 px-3 py-3 text-slate-300">
                  {aircraft.lastTelemetry}
                </td>
                <td className="border border-l-0 border-r-0 border-slate-700 px-3 py-3">
                  <span className={`inline-flex items-center rounded-sm border px-2 py-1 text-[10px] font-medium uppercase tracking-[0.12em] ${authStyles[aircraft.authentication]}`}>
                    {aircraft.authentication}
                  </span>
                </td>
                <td className="border border-l-0 border-r-0 border-slate-700 px-3 py-3 text-slate-300">
                  {aircraft.altitude}
                </td>
                <td className="rounded-r-sm border border-l-0 border-slate-700 px-3 py-3 text-slate-300">
                  {aircraft.speed}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}
