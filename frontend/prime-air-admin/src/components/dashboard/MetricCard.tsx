type MetricCardProps = {
  label: string
  value: string
  description: string
}

export function MetricCard({ label, value, description }: MetricCardProps) {
  return (
    <div className="rounded-sm border border-slate-700 bg-[#0c1624] p-4">
      <div className="mb-3">
        <span className="text-[10px] font-semibold uppercase tracking-[0.18em] text-slate-400">
          {label}
        </span>
      </div>
      <div className="text-3xl font-semibold tracking-[-0.04em] text-slate-50">{value}</div>
      <div className="mt-2 text-xs text-slate-400">{description}</div>
    </div>
  )
}
