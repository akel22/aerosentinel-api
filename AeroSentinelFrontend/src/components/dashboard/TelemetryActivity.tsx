import Chart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import { TelemetryTrendPoint } from "../../services/api";

interface TelemetryActivityProps {
  telemetryTrend?: TelemetryTrendPoint[];
}

export default function TelemetryActivity({ telemetryTrend = [] }: TelemetryActivityProps) {
  // Default data if no telemetry trend provided
  const defaultCategories = ["00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00"];
  const defaultData = [10, 20, 15, 25, 18, 22, 28, 32, 26, 30, 24, 28];
  
  const categories = telemetryTrend.length > 0 ? telemetryTrend.map(t => t.hour) : defaultCategories;
  const data = telemetryTrend.length > 0 ? telemetryTrend.map(t => t.count) : defaultData;

  const options: ApexOptions = {
    colors: ["#465fff"],
    chart: {
      fontFamily: "Outfit, sans-serif",
      type: "bar",
      height: 180,
      toolbar: {
        show: false,
      },
    },
    plotOptions: {
      bar: {
        horizontal: false,
        columnWidth: "39%",
        borderRadius: 5,
        borderRadiusApplication: "end",
      },
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      show: true,
      width: 4,
      colors: ["transparent"],
    },
    xaxis: {
      categories: categories,
      axisBorder: {
        show: false,
      },
      axisTicks: {
        show: false,
      },
    },
    legend: {
      show: true,
      position: "top",
      horizontalAlign: "left",
      fontFamily: "Outfit",
    },
    yaxis: {
      title: {
        text: undefined,
      },
    },
    grid: {
      yaxis: {
        lines: {
          show: true,
        },
      },
    },
    fill: {
      opacity: 1,
    },

    tooltip: {
      x: {
        show: false,
      },
      y: {
        formatter: (val: number) => `${val} records`,
      },
    },
  };
  const series = [
    {
      name: "Telemetry Records",
      data: data,
    },
  ];

  return (
    <div className="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] sm:p-6">
      <div className="flex items-center justify-between mb-5.5">
        <div>
          <h4 className="text-lg font-semibold text-gray-800 dark:text-white/90">
            Telemetry Activity
          </h4>
          <p className="text-gray-500 text-theme-sm dark:text-gray-400">
            Hourly telemetry records received
          </p>
        </div>  
      </div>

      <div>
        <div id="chartOne" className="-ml-5">
          <Chart options={options} series={series} type="bar" height={180} />
        </div>
      </div>
    </div>
  );
}
