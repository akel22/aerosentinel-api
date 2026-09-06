import { BrowserRouter as Router, Routes, Route } from "react-router";
import AppLayout from "./layout/AppLayout";
import { ScrollToTop } from "./components/common/ScrollToTop";
import Home from "./Main/Dashboard";
import NotFound from "./ReusablePages/NotFound";
import UserProfiles from "./Main/UserProfile";
import WaypointsPage from "./Main/WaypointsPage";

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

export default function App() {
  return (
    <>
      <Router>
        <ScrollToTop />
        <Routes>
          <Route element={<AppLayout />}>
            <Route index path="/" element={<Home />} />
            <Route path="/fleet" element={<FleetPage />} />
            <Route path="/profile" element={<UserProfiles />} />
            <Route path="/telemetry" element={<TelemetryPage />} />
            <Route path="/security" element={<SecurityPage />} />
            <Route path="/incidents" element={<WaypointsPage />} />
          </Route>

          <Route path="*" element={<NotFound />} />
        </Routes>
      </Router>
    </>
  );
}
