import { ShieldCheck } from "lucide-react";
import type { ActivityItem } from "../../types";
import { formatDateTime } from "../../utils/format";
import { StatusPill } from "./StatusPill";

interface ActivityLogListProps {
  title: string;
  items: ActivityItem[];
}

export function ActivityLogList({ title, items }: ActivityLogListProps) {
  return (
    <section className="app-card p-5">
      <div className="mb-5 flex items-center justify-between">
        <h2 className="text-base font-semibold text-brand-black">{title}</h2>
        <ShieldCheck className="h-5 w-5 text-brand-gold" />
      </div>
      <div className="space-y-4">
        {items.map((item) => (
          <article key={item.id} className="flex gap-3">
            <div className="flex h-10 w-10 shrink-0 items-center justify-center rounded-full bg-brand-black text-xs font-bold text-brand-gold">
              {item.title.slice(0, 2).toUpperCase()}
            </div>
            <div className="min-w-0 flex-1">
              <div className="flex flex-wrap items-center gap-2">
                <p className="truncate text-sm font-semibold text-brand-black">
                  {item.title}
                </p>
                {item.tag ? <StatusPill label={item.tag} tone={item.tone} /> : null}
              </div>
              <p className="mt-1 text-sm text-zinc-500">{item.subtitle}</p>
              <p className="mt-1 text-xs font-medium text-zinc-400">
                {formatDateTime(item.timestamp)}
              </p>
            </div>
          </article>
        ))}
      </div>
    </section>
  );
}
