import { BrowserRouter as Router, Routes, Route } from "react-router";
import AppLayout from "./layout/AppLayout";
import { ScrollToTop } from "./components/common/ScrollToTop";
import Home from "./pages/Dashboard/Home";
import NotFound from "./pages/OtherPage/NotFound";

const FleetPage = () => (
  <div className="aero-shell p-6">
    <div className="aero-card p-8">
      <p className="text-xs uppercase tracking-[0.28em] text-cyan-300">Fleet</p>
      <h2 className="mt-3 text-3xl font-semibold text-white">Aircraft fleet overview</h2>
      <p className="mt-3 max-w-2xl text-slate-300">
        This view is ready for aircraft roster, maintenance windows, range coverage, and dispatch readiness telemetry.
      </p>
    </div>
  </div>
);

const FlightOpsPage = () => (
  <div className="aero-shell p-6">
    <div className="aero-card p-8">
      <p className="text-xs uppercase tracking-[0.28em] text-cyan-300">Flight operations</p>
      <h2 className="mt-3 text-3xl font-semibold text-white">Dispatch and route control</h2>
      <p className="mt-3 max-w-2xl text-slate-300">
        Coordinate delayed flights, live route compliance, and ETD/ETA health signals from a single command surface.
      </p>
    </div>
  </div>
);

const TelemetryPage = () => (
  <div className="aero-shell p-6">
    <div className="aero-card p-8">
      <p className="text-xs uppercase tracking-[0.28em] text-cyan-300">Telemetry</p>
      <h2 className="mt-3 text-3xl font-semibold text-white">Signal integrity monitor</h2>
      <p className="mt-3 max-w-2xl text-slate-300">
        Monitor line-of-sight quality, fresh telemetry streams, and signal integrity across the active airspace network.
      </p>
    </div>
  </div>
);

const SecurityPage = () => (
  <div className="aero-shell p-6">
    <div className="aero-card p-8">
      <p className="text-xs uppercase tracking-[0.28em] text-cyan-300">Security</p>
      <h2 className="mt-3 text-3xl font-semibold text-white">Threat detection and validation</h2>
      <p className="mt-3 max-w-2xl text-slate-300">
        Review verification rates, suspicious activity, and potential spoofing or credential anomalies across the fleet.
      </p>
    </div>
  </div>
);

const IncidentsPage = () => (
  <div className="aero-shell p-6">
    <div className="aero-card p-8">
      <p className="text-xs uppercase tracking-[0.28em] text-cyan-300">Incidents</p>
      <h2 className="mt-3 text-3xl font-semibold text-white">Operational incident log</h2>
      <p className="mt-3 max-w-2xl text-slate-300">
        Track escalations, failed telemetry, and operational anomalies before they impact critical flight operations.
      </p>
    </div>
  </div>
);

export default function App() {
  return (
    <>
      <Router>
        <ScrollToTop />
        <Routes>
          <Route element={<AppLayout />}>
            <Route index path="/" element={<Home />} />
            <Route path="/fleet" element={<FleetPage />} />
            <Route path="/flight-ops" element={<FlightOpsPage />} />
            <Route path="/telemetry" element={<TelemetryPage />} />
            <Route path="/security" element={<SecurityPage />} />
            <Route path="/incidents" element={<IncidentsPage />} />
          </Route>

          <Route path="*" element={<NotFound />} />
        </Routes>
      </Router>
    </>
  );
}
