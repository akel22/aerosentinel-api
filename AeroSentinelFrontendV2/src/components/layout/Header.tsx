import { Bell, ChevronDown, Menu, Search } from "lucide-react";
import { useState } from "react";

interface HeaderProps {
  title: string;
  breadcrumb: string;
  onMenuClick: () => void;
}

export function Header({ title, breadcrumb, onMenuClick }: HeaderProps) {
  const [profileOpen, setProfileOpen] = useState(false);

  return (
    <header className="sticky top-0 z-20 border-b border-zinc-200 bg-white">
      <div className="flex min-h-20 flex-col gap-4 px-4 py-4 sm:px-6 xl:flex-row xl:items-center xl:justify-between">
        <div className="flex items-center gap-3">
          <button type="button" className="icon-button lg:hidden" onClick={onMenuClick}>
            <Menu className="h-5 w-5" />
          </button>
          <div>
            <p className="text-xs font-semibold uppercase tracking-wide text-zinc-500">
              {breadcrumb}
            </p>
            <h1 className="mt-1 text-2xl font-bold tracking-normal text-brand-black">
              {title}
            </h1>
          </div>
        </div>

        <div className="flex flex-col gap-3 sm:flex-row sm:items-center">
          <label className="relative block w-full sm:w-80">
            <Search className="pointer-events-none absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-zinc-400" />
            <input
              className="field pl-9"
              type="search"
              placeholder="Search aircraft, waypoints, logs"
            />
          </label>
          <button type="button" className="icon-button">
            <Bell className="h-5 w-5" />
          </button>
          <div className="relative">
            <button
              type="button"
              className="flex h-11 items-center gap-3 rounded-xl border border-zinc-200 bg-white px-2 pr-3 text-left transition hover:border-brand-gold focus:outline-none focus:ring-2 focus:ring-brand-gold/40"
              onClick={() => setProfileOpen((current) => !current)}
            >
              <span className="flex h-8 w-8 items-center justify-center rounded-full bg-brand-black text-xs font-bold text-brand-gold">
                ER
              </span>
              <span className="hidden sm:block">
                <span className="block text-sm font-semibold text-brand-black">
                  Elijah Revel
                </span>
                <span className="block text-xs text-zinc-500">Ops Admin</span>
              </span>
              <ChevronDown className="h-4 w-4 text-zinc-500" />
            </button>

            {profileOpen ? (
              <div className="absolute right-0 mt-2 w-52 rounded-xl border border-zinc-200 bg-white p-2 shadow-panel">
                {["Profile", "Security", "Sign out"].map((item) => (
                  <button
                    key={item}
                    type="button"
                    className="block w-full rounded-lg px-3 py-2 text-left text-sm font-medium text-zinc-600 hover:bg-zinc-100 hover:text-brand-black"
                  >
                    {item}
                  </button>
                ))}
              </div>
            ) : null}
          </div>
        </div>
      </div>
    </header>
  );
}
