import { useEffect, useState } from "react";
import PageMeta from "../components/common/PageMeta";
import { dashboardApi, DashboardSnapshot } from "../services/api";
import TelemetryActivity from "../components/dashboard/TelemetryActivity";
import VerificationCard from "../components/dashboard/VerificationCard";
import AircraftStatus from "../components/dashboard/AircraftStatus";
import RecentFlights from "../components/dashboard/RecentFlights";
import BriefCard from "../components/dashboard/BriefCard";
import PageBreadcrumb from "../components/common/PageBreadCrumb";

export default function Home() {
  const [dashboardData, setDashboardData] = useState<DashboardSnapshot | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);
        const data = await dashboardApi.getDashboardSnapshot();
        setDashboardData(data);
      } catch (err) {
        console.error("Error fetching dashboard data:", err);
        setError("Failed to load dashboard data. Please try again later.");
        setLoading(false);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
    // Optionally refresh data every 15 seconds
    const interval = setInterval(fetchData, 5000);

    return () => clearInterval(interval);
  }, []);

  if (loading && !dashboardData) {
    return (
      <div className="flex items-center justify-center h-screen">
         <div role="status">
      <svg xmlns="http://www.w3.org/2000/svg" className="size-9 shrink-0 animate-spin dark:fill-slate-50"
         viewBox="0 0 24 24" aria-hidden="true">
         <path
            d="M12 3.268c-.708 0-1.298-.59-1.298-1.298S11.292.672 12 .672s1.298.59 1.298 1.298-.59 1.298-1.298 1.298zm0-1.416c-.118 0-.118 0-.118.118s.236.118.236 0 0-.118-.118-.118zM7.044 4.566c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.59 1.298-1.298 1.298zm0-1.416c-.118 0-.118 0-.118.118s.236.118.236 0l-.118-.118zM3.268 8.342c-.708 0-1.298-.59-1.298-1.298s.59-1.416 1.298-1.416 1.298.59 1.298 1.298-.59 1.416-1.298 1.416zm0-1.534c-.118 0-.118 0-.118.118s.236.118.236 0c0 0 0-.118-.118-.118zm-1.298 6.49c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.59 1.298-1.298 1.298zm0-1.416c-.118 0-.118 0-.118.118s.236.118.236 0 0-.118-.118-.118zM12 23.328c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.59 1.298-1.298 1.298zm0-1.416c-.118 0-.118 0-.118.118s.236.118.236 0 0-.118-.118-.118zm4.956.118c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.472 1.298-1.298 1.298zm0-1.416c0 .236.118.236 0 0 .118 0 .118 0 0 0zm3.776-2.36c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.59 1.298-1.298 1.298zm0-1.298c-.118 0-.118 0 0 0-.118.236.118.236 0 0zm1.298-3.658c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.59 1.298-1.298 1.298zm0-1.416c-.118 0-.118 0-.118.118s.236.118.236 0 0-.118-.118-.118zM3.268 18.254c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.59 1.298-1.298 1.298zm0-1.298c-.118 0-.118 0-.118.118s.236.118.236 0 0-.118-.118-.118zm17.464-8.614c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.59 1.298-1.298 1.298zm0-1.534c-.118 0-.118 0-.118.118s.236.118.236 0l-.118-.118zM7.044 22.03c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.59 1.298-1.298 1.298zm0-1.416c-.118 0-.118 0-.118.118s.236.118.236 0-.118-.118-.118-.118zm9.912-16.048c-.708 0-1.298-.59-1.298-1.298s.59-1.298 1.298-1.298 1.298.59 1.298 1.298-.472 1.298-1.298 1.298zm0-1.416c-.118 0-.118 0-.118.118s.236.118.236 0c0 0 0-.118-.118-.118z"
            data-original="#000000" />
      </svg>
      <span className="sr-only">Loading…</span>
   </div>
      </div>
    );
  }

  if (error && !dashboardData) {
    return (
      <div className="flex items-center justify-center h-screen">
        <div className="text-center">
          <p className="text-red-500">{error}</p>
        </div>
      </div>
    );
  }

  const summary = dashboardData?.summary;
  const telemetryTrend = dashboardData?.telemetryTrend || [];
  const aircraftStatus = dashboardData?.aircraftStatus || [];
  const failedTelemetry = dashboardData?.failedTelemetry || [];
  const finishedFlights = dashboardData?.finishedFlights || [];

  return (
    <>
      <PageMeta
        title="Aviation Telemetry Dashboard | AeroSentinel"
        description="Real-time aviation telemetry monitoring and flight tracking dashboard"
      />
      
      <div className="grid grid-cols-12 gap-4 md:gap-6">
        <div className="col-span-12 space-y-6 xl:col-span-7">
          <BriefCard dashboardData={summary} />

          <TelemetryActivity telemetryTrend={telemetryTrend} />
        </div>

        <div className="col-span-12 xl:col-span-5">
          <VerificationCard verificationPercentage={summary?.verificationPercentage} />
        </div>

        <div className="col-span-12">
          <AircraftStatus aircraftStatus={aircraftStatus} />
        </div>

        <div className="col-span-12 xl:col-span-5">
          <AircraftStatus aircraftStatus={aircraftStatus} />
        </div>

        <div className="col-span-12 xl:col-span-7">
          <RecentFlights finishedFlights={finishedFlights} failedTelemetry={failedTelemetry} />
        </div>
      </div>
    </>
  );
}
