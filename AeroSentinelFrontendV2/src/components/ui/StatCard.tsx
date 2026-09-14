import type { ReactNode } from "react";

interface StatCardProps {
  label: string;
  value: string;
  supportingText: string;
  icon: ReactNode;
  trend?: string;
}

export function StatCard({
  label,
  value,
  supportingText,
  icon,
  trend,
}: StatCardProps) {
  return (
    <article className="app-card p-5">
      <div className="flex items-start justify-between gap-4">
        <div>
          <p className="text-sm font-medium text-zinc-500">{label}</p>
          <p className="mt-3 text-3xl font-bold tracking-normal text-brand-black">
            {value}
          </p>
        </div>
        <div className="flex h-11 w-11 shrink-0 items-center justify-center rounded-xl bg-brand-gold/20 text-brand-black">
          {icon}
        </div>
      </div>
      <div className="mt-4 flex items-center justify-between gap-3 border-t border-zinc-100 pt-4">
        <span className="text-sm text-zinc-500">{supportingText}</span>
        {trend ? (
          <span className="rounded-full bg-brand-black px-2.5 py-1 text-xs font-semibold text-white">
            {trend}
          </span>
        ) : null}
      </div>
    </article>
  );
}
