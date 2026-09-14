import {
  BarChart3,
  Gauge,
  MapPinned,
  RadioTower,
  Settings,
  ShieldCheck,
  X,
} from "lucide-react";
import type { LucideIcon } from "lucide-react";
import type { PageId } from "../../types";
import { cx } from "../../utils/format";

const navItems: Array<{ id: PageId; label: string; icon: LucideIcon }> = [
  { id: "dashboard", label: "Dashboard", icon: Gauge },
  { id: "analytics", label: "Analytics", icon: BarChart3 },
  { id: "settings", label: "Settings", icon: Settings },
  { id: "waypoints", label: "Waypoints", icon: MapPinned },
  { id: "telemetry", label: "Telemetry Logs", icon: RadioTower },
];

interface SidebarProps {
  activePage: PageId;
  mobileOpen: boolean;
  onClose: () => void;
  onNavigate: (page: PageId) => void;
}

export function Sidebar({
  activePage,
  mobileOpen,
  onClose,
  onNavigate,
}: SidebarProps) {
  return (
    <>
      <div
        className={cx(
          "fixed inset-0 z-30 bg-brand-black/40 transition lg:hidden",
          mobileOpen ? "opacity-100" : "pointer-events-none opacity-0",
        )}
        onClick={onClose}
      />
      <aside
        className={cx(
          "fixed inset-y-0 left-0 z-40 flex w-72 flex-col bg-brand-black text-white transition-transform duration-200 lg:translate-x-0",
          mobileOpen ? "translate-x-0" : "-translate-x-full",
        )}
      >
        <div className="flex h-20 items-center justify-between border-b border-white/10 px-5">
          <div className="flex items-center gap-3">
            <div className="flex h-11 w-11 items-center justify-center rounded-xl bg-brand-gold text-brand-black">
              <ShieldCheck className="h-6 w-6" />
            </div>
            <div>
              <p className="text-lg font-bold tracking-normal">AeroSentinel</p>
              <p className="text-xs font-medium text-zinc-400">Admin V2</p>
            </div>
          </div>
          <button
            type="button"
            className="icon-button border-white/10 bg-transparent text-white hover:bg-white/10 lg:hidden"
            onClick={onClose}
          >
            <X className="h-5 w-5" />
          </button>
        </div>

        <nav className="flex-1 space-y-1 px-3 py-5">
          {navItems.map((item) => {
            const Icon = item.icon;
            const isActive = item.id === activePage;

            return (
              <button
                key={item.id}
                type="button"
                className={cx(
                  "flex w-full items-center gap-3 rounded-xl px-3 py-3 text-left text-sm font-semibold transition",
                  isActive
                    ? "bg-brand-gold text-brand-black"
                    : "text-zinc-300 hover:bg-white/10 hover:text-white",
                )}
                onClick={() => {
                  onNavigate(item.id);
                  onClose();
                }}
              >
                <Icon className="h-5 w-5 shrink-0" />
                <span>{item.label}</span>
              </button>
            );
          })}
        </nav>

        <div className="border-t border-white/10 p-5">
          <p className="text-xs font-semibold uppercase tracking-wide text-zinc-500">
            Backend
          </p>
          <p className="mt-2 text-sm font-semibold text-white">
            http://localhost:5253
          </p>
        </div>
      </aside>
    </>
  );
}
