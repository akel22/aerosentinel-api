import { SettingsForm } from "../components/settings/SettingsForm";

export function SettingsPage() {
  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-3xl font-bold tracking-normal text-brand-black">
          Control Center Settings
        </h2>
        <p className="mt-1 text-sm text-zinc-500">
          Profile, notifications, security, team, and API preferences
        </p>
      </div>
      <SettingsForm />
    </div>
  );
}
