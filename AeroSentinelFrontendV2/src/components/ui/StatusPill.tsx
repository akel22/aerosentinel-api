import type { StatusTone } from "../../types";

const toneClasses: Record<StatusTone, string> = {
  success: "border-emerald-200 bg-emerald-50 text-emerald-700",
  warning: "border-amber-200 bg-amber-50 text-amber-800",
  danger: "border-red-200 bg-red-50 text-red-700",
  neutral: "border-zinc-200 bg-zinc-100 text-zinc-700",
  gold: "border-brand-gold/70 bg-brand-gold/20 text-brand-black",
};

interface StatusPillProps {
  label: string;
  tone?: StatusTone;
}

export function StatusPill({ label, tone = "neutral" }: StatusPillProps) {
  return (
    <span
      className={`inline-flex min-w-20 items-center justify-center rounded-full border px-2.5 py-1 text-xs font-semibold capitalize ${toneClasses[tone]}`}
    >
      {label}
    </span>
  );
}
