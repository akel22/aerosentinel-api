import { useEffect, useMemo, useState } from "react";
import type { PageId } from "../../types";
import { Analytics } from "../../pages/Analytics";
import { Dashboard } from "../../pages/Dashboard";
import { SettingsPage } from "../../pages/Settings";
import { TelemetryLogs } from "../../pages/TelemetryLogs";
import { Waypoints } from "../../pages/Waypoints";
import { Footer } from "./Footer";
import { Header } from "./Header";
import { Sidebar } from "./Sidebar";

const pageLabels: Record<PageId, { title: string; breadcrumb: string }> = {
  dashboard: { title: "Dashboard", breadcrumb: "Operations / Dashboard" },
  analytics: { title: "Analytics", breadcrumb: "Operations / Analytics" },
  settings: { title: "Settings", breadcrumb: "Admin / Settings" },
  waypoints: { title: "Waypoints", breadcrumb: "Airspace / Waypoints" },
  telemetry: { title: "Telemetry Logs", breadcrumb: "Security / Telemetry" },
};

const pageIds: PageId[] = [
  "dashboard",
  "analytics",
  "settings",
  "waypoints",
  "telemetry",
];

function readPageFromHash(): PageId {
  const slug = window.location.hash.replace(/^#\/?/, "");
  return pageIds.includes(slug as PageId) ? (slug as PageId) : "dashboard";
}

export function AppShell() {
  const [activePage, setActivePage] = useState<PageId>(readPageFromHash);
  const [mobileOpen, setMobileOpen] = useState(false);
  const meta = pageLabels[activePage];

  useEffect(() => {
    const listener = () => setActivePage(readPageFromHash());
    window.addEventListener("hashchange", listener);

    return () => window.removeEventListener("hashchange", listener);
  }, []);

  const page = useMemo(() => {
    switch (activePage) {
      case "analytics":
        return <Analytics />;
      case "settings":
        return <SettingsPage />;
      case "waypoints":
        return <Waypoints />;
      case "telemetry":
        return <TelemetryLogs />;
      case "dashboard":
      default:
        return <Dashboard />;
    }
  }, [activePage]);

  function handleNavigate(pageId: PageId) {
    window.location.hash = `/${pageId}`;
    setActivePage(pageId);
  }

  return (
    <div className="min-h-screen bg-zinc-50">
      <Sidebar
        activePage={activePage}
        mobileOpen={mobileOpen}
        onClose={() => setMobileOpen(false)}
        onNavigate={handleNavigate}
      />
      <div className="min-h-screen lg:pl-72">
        <Header
          title={meta.title}
          breadcrumb={meta.breadcrumb}
          onMenuClick={() => setMobileOpen(true)}
        />
        <main className="mx-auto w-full max-w-[1600px] px-4 py-6 sm:px-6 lg:px-8">
          {page}
        </main>
        <Footer />
      </div>
    </div>
  );
}
