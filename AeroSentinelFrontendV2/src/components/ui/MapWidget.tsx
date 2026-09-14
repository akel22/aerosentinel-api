import { Plane } from "lucide-react";
import { useMemo, useState } from "react";
import type { MapMarker } from "../../types";
import { toneFromStatus } from "../../utils/status";
import { StatusPill } from "./StatusPill";

interface MapWidgetProps {
  markers: MapMarker[];
}

export function MapWidget({ markers }: MapWidgetProps) {
  const firstMarker = markers[0];
  const [selectedMarkerId, setSelectedMarkerId] = useState(
    firstMarker?.id ?? "",
  );
  const selectedMarker = useMemo(
    () => markers.find((marker) => marker.id === selectedMarkerId) ?? firstMarker,
    [firstMarker, markers, selectedMarkerId],
  );

  return (
    <section className="app-card p-5">
      <div className="mb-5 flex flex-col gap-2 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-base font-semibold text-brand-black">
            Aircraft Location Map
          </h2>
          <p className="mt-1 text-sm text-zinc-500">
            Active aircraft and waypoint corridor positions
          </p>
        </div>
        <StatusPill label={`${markers.length} tracked`} tone="gold" />
      </div>
      <div className="relative min-h-[330px] overflow-hidden rounded-xl border border-brand-black bg-brand-black text-white">
        <div className="absolute inset-5 rounded-xl border border-white/10" />
        <div className="absolute left-6 right-6 top-1/3 h-px bg-white/10" />
        <div className="absolute left-6 right-6 top-2/3 h-px bg-white/10" />
        <div className="absolute bottom-6 top-6 left-1/3 w-px bg-white/10" />
        <div className="absolute bottom-6 top-6 left-2/3 w-px bg-white/10" />
        <div className="absolute left-[37%] top-[24%] h-24 w-16 rounded-[50%] border border-white/20 bg-white/5" />
        <div className="absolute left-[47%] top-[41%] h-32 w-20 rotate-12 rounded-[45%] border border-white/20 bg-white/5" />
        <div className="absolute left-[57%] top-[61%] h-24 w-20 -rotate-12 rounded-[45%] border border-white/20 bg-white/5" />
        <div className="absolute left-5 top-5 rounded-lg border border-white/10 bg-white px-3 py-2 text-xs font-semibold uppercase tracking-wide text-brand-black">
          PH FIR
        </div>
        {markers.map((marker) => (
          <button
            key={marker.id}
            type="button"
            aria-label={`Select ${marker.label}`}
            className="absolute z-10 flex -translate-x-1/2 -translate-y-1/2 items-center gap-1 rounded-full border border-brand-gold bg-brand-gold px-2 py-1 text-xs font-bold text-brand-black shadow-sm transition hover:scale-105 focus:outline-none focus:ring-2 focus:ring-white"
            style={{ top: marker.top, left: marker.left }}
            onClick={() => setSelectedMarkerId(marker.id)}
          >
            <Plane className="h-3.5 w-3.5" />
            {marker.label}
          </button>
        ))}
        {selectedMarker ? (
          <div className="absolute bottom-5 right-5 z-20 w-[min(18rem,calc(100%-2.5rem))] rounded-xl border border-white/10 bg-white p-4 text-brand-black shadow-panel">
            <div className="flex items-start justify-between gap-3">
              <div>
                <p className="text-xs font-semibold uppercase tracking-wide text-zinc-500">
                  Selected Aircraft
                </p>
                <h3 className="mt-1 text-lg font-bold">{selectedMarker.label}</h3>
              </div>
              <StatusPill
                label={selectedMarker.status}
                tone={toneFromStatus(selectedMarker.status)}
              />
            </div>
            <dl className="mt-4 grid grid-cols-2 gap-3 text-sm">
              <div>
                <dt className="text-zinc-500">Location</dt>
                <dd className="mt-1 font-semibold">{selectedMarker.location}</dd>
              </div>
              <div>
                <dt className="text-zinc-500">Updated</dt>
                <dd className="mt-1 font-semibold">{selectedMarker.updatedAt}</dd>
              </div>
            </dl>
          </div>
        ) : null}
      </div>
    </section>
  );
}
