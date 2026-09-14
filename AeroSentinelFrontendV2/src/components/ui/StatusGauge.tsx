interface StatusGaugeProps {
  value: number;
  label: string;
  description: string;
}

export function StatusGauge({ value, label, description }: StatusGaugeProps) {
  const normalizedValue = Math.max(0, Math.min(100, value));
  const radius = 58;
  const circumference = 2 * Math.PI * radius;
  const offset = circumference - (normalizedValue / 100) * circumference;

  return (
    <section className="app-card p-5">
      <div className="flex items-center justify-between gap-4">
        <div>
          <h2 className="text-base font-semibold text-brand-black">{label}</h2>
          <p className="mt-1 text-sm text-zinc-500">{description}</p>
        </div>
        <span className="rounded-full border border-brand-gold bg-brand-gold/20 px-3 py-1 text-xs font-semibold text-brand-black">
          Live
        </span>
      </div>
      <div className="mt-6 flex items-center justify-center">
        <div className="relative h-40 w-40">
          <svg className="h-40 w-40 -rotate-90" viewBox="0 0 140 140">
            <circle
              cx="70"
              cy="70"
              r={radius}
              fill="none"
              stroke="#E4E4E7"
              strokeWidth="12"
            />
            <circle
              cx="70"
              cy="70"
              r={radius}
              fill="none"
              stroke="#DEC154"
              strokeLinecap="round"
              strokeWidth="12"
              strokeDasharray={circumference}
              strokeDashoffset={offset}
            />
          </svg>
          <div className="absolute inset-0 flex flex-col items-center justify-center">
            <span className="text-4xl font-bold text-brand-black">
              {Math.round(normalizedValue)}%
            </span>
            <span className="text-xs font-semibold uppercase tracking-wide text-zinc-500">
              Operational
            </span>
          </div>
        </div>
      </div>
    </section>
  );
}
