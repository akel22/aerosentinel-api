import { Bell, KeyRound, ServerCog, Shield, UserRound, Users } from "lucide-react";
import { useState } from "react";
import { cx } from "../../utils/format";

type SettingsSection = "profile" | "notifications" | "security" | "team" | "api";

const settingsNav: Array<{
  id: SettingsSection;
  label: string;
  icon: typeof UserRound;
}> = [
  { id: "profile", label: "Profile", icon: UserRound },
  { id: "notifications", label: "Notifications", icon: Bell },
  { id: "security", label: "Security", icon: Shield },
  { id: "team", label: "Team", icon: Users },
  { id: "api", label: "System API", icon: ServerCog },
];

function Toggle({ label, defaultChecked }: { label: string; defaultChecked?: boolean }) {
  const [checked, setChecked] = useState(Boolean(defaultChecked));

  return (
    <label className="flex items-center justify-between gap-4 rounded-xl border border-zinc-200 px-4 py-3">
      <span className="text-sm font-medium text-brand-black">{label}</span>
      <button
        type="button"
        className={cx(
          "relative h-6 w-11 rounded-full border transition focus:outline-none focus:ring-2 focus:ring-brand-gold/40",
          checked
            ? "border-brand-gold bg-brand-gold"
            : "border-zinc-300 bg-zinc-200",
        )}
        onClick={() => setChecked((current) => !current)}
        aria-pressed={checked}
      >
        <span
          className={cx(
            "absolute top-0.5 h-5 w-5 rounded-full bg-white transition",
            checked ? "left-5" : "left-0.5",
          )}
        />
      </button>
    </label>
  );
}

export function SettingsForm() {
  const [activeSection, setActiveSection] = useState<SettingsSection>("profile");
  const activeItem = settingsNav.find((item) => item.id === activeSection);

  return (
    <div className="grid gap-6 lg:grid-cols-[17rem_1fr]">
      <aside className="app-card p-3">
        <nav className="flex gap-2 overflow-x-auto lg:flex-col">
          {settingsNav.map((item) => {
            const Icon = item.icon;
            const isActive = item.id === activeSection;

            return (
              <button
                key={item.id}
                type="button"
                className={cx(
                  "flex min-w-max items-center gap-3 rounded-xl px-3 py-3 text-sm font-semibold transition",
                  isActive
                    ? "bg-brand-black text-white"
                    : "text-zinc-600 hover:bg-zinc-100 hover:text-brand-black",
                )}
                onClick={() => setActiveSection(item.id)}
              >
                <Icon className="h-4 w-4 text-brand-gold" />
                {item.label}
              </button>
            );
          })}
        </nav>
      </aside>

      <section className="app-card p-5">
        <div className="border-b border-zinc-200 pb-5">
          <p className="text-sm font-semibold uppercase tracking-wide text-brand-gold">
            Admin Settings
          </p>
          <h2 className="mt-2 text-2xl font-bold text-brand-black">
            {activeItem?.label}
          </h2>
        </div>

        {activeSection === "profile" ? (
          <form className="mt-6 grid gap-5 md:grid-cols-2">
            <label className="space-y-2">
              <span className="text-sm font-semibold text-brand-black">
                Full name
              </span>
              <input className="field" defaultValue="Elijah Revel" />
            </label>
            <label className="space-y-2">
              <span className="text-sm font-semibold text-brand-black">Role</span>
              <input className="field" defaultValue="Operations Admin" />
            </label>
            <label className="space-y-2">
              <span className="text-sm font-semibold text-brand-black">
                Email
              </span>
              <input className="field" defaultValue="ops@aerosentinel.local" />
            </label>
            <label className="space-y-2">
              <span className="text-sm font-semibold text-brand-black">
                Time zone
              </span>
              <select className="field" defaultValue="Asia/Manila">
                <option>Asia/Manila</option>
                <option>UTC</option>
                <option>America/Los_Angeles</option>
              </select>
            </label>
          </form>
        ) : null}

        {activeSection === "notifications" ? (
          <div className="mt-6 grid gap-4">
            <Toggle label="Critical telemetry failure alerts" defaultChecked />
            <Toggle label="Daily fleet health report" defaultChecked />
            <Toggle label="Waypoint import completion notices" />
            <Toggle label="Credential rotation reminders" defaultChecked />
          </div>
        ) : null}

        {activeSection === "security" ? (
          <div className="mt-6 grid gap-5 lg:grid-cols-2">
            <div className="rounded-xl border border-zinc-200 p-4">
              <KeyRound className="h-5 w-5 text-brand-gold" />
              <h3 className="mt-3 text-base font-semibold text-brand-black">
                Access Control
              </h3>
              <div className="mt-4 grid gap-3">
                <Toggle label="Two-factor authentication" defaultChecked />
                <Toggle label="Require session revalidation" defaultChecked />
              </div>
            </div>
            <div className="rounded-xl border border-zinc-200 p-4">
              <Shield className="h-5 w-5 text-brand-gold" />
              <h3 className="mt-3 text-base font-semibold text-brand-black">
                Session Management
              </h3>
              <label className="mt-4 block space-y-2">
                <span className="text-sm font-semibold text-brand-black">
                  Session timeout
                </span>
                <select className="field" defaultValue="30 minutes">
                  <option>15 minutes</option>
                  <option>30 minutes</option>
                  <option>1 hour</option>
                </select>
              </label>
            </div>
          </div>
        ) : null}

        {activeSection === "team" ? (
          <div className="mt-6 overflow-hidden rounded-xl border border-zinc-200">
            {[
              ["Mara Santos", "Flight Ops", "Admin"],
              ["Jon Reyes", "Telemetry", "Analyst"],
              ["Aya Cruz", "Security", "Reviewer"],
            ].map(([name, team, role]) => (
              <div
                key={name}
                className="grid gap-3 border-b border-zinc-100 px-4 py-4 last:border-b-0 md:grid-cols-3"
              >
                <span className="font-semibold text-brand-black">{name}</span>
                <span className="text-zinc-500">{team}</span>
                <span className="text-zinc-500">{role}</span>
              </div>
            ))}
          </div>
        ) : null}

        {activeSection === "api" ? (
          <form className="mt-6 grid gap-5 md:grid-cols-2">
            <label className="space-y-2 md:col-span-2">
              <span className="text-sm font-semibold text-brand-black">
                API base URL
              </span>
              <input className="field" defaultValue="http://localhost:5253" />
            </label>
            <label className="space-y-2">
              <span className="text-sm font-semibold text-brand-black">
                Retry policy
              </span>
              <select className="field" defaultValue="Standard">
                <option>Standard</option>
                <option>Aggressive</option>
                <option>Manual only</option>
              </select>
            </label>
            <label className="space-y-2">
              <span className="text-sm font-semibold text-brand-black">
                Data freshness target
              </span>
              <select className="field" defaultValue="Under 2 minutes">
                <option>Under 1 minute</option>
                <option>Under 2 minutes</option>
                <option>Under 5 minutes</option>
              </select>
            </label>
          </form>
        ) : null}

        <div className="mt-8 flex justify-end gap-3 border-t border-zinc-200 pt-5">
          <button
            type="button"
            className="rounded-xl border border-zinc-200 px-4 py-2 text-sm font-semibold text-zinc-700 transition hover:border-brand-black hover:text-brand-black"
          >
            Reset
          </button>
          <button
            type="button"
            className="rounded-xl bg-brand-black px-4 py-2 text-sm font-semibold text-white transition hover:bg-zinc-800 focus:outline-none focus:ring-2 focus:ring-brand-gold/60"
          >
            Save Changes
          </button>
        </div>
      </section>
    </div>
  );
}
