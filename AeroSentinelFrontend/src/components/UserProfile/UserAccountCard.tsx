import { useState } from "react";
import EditProfileModal from "./Modals/EditProfileModal";
import { PencilIcon } from "../../icons";

export interface UserAccountDetails {
  email: string;
  commandLine: string;
  stationSector: string;
  dispatchId: string;
  commsFrequency: string;
  clearanceCert: string;
}

interface UserAccountCardProps {
  data?: UserAccountDetails;
  onUpdate?: (updated: UserAccountDetails) => void;
}

export default function UserAccountCard({ data, onUpdate }: UserAccountCardProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [accountData, setAccountData] = useState<UserAccountDetails>(
    data || {
      email: "admin@primeair.ph",
      commandLine: "",
      stationSector: "",
      dispatchId: "MNL-DISPATCH-01",
      commsFrequency: "",
      clearanceCert: "CAAP Admin Badge #8841",
    }
  );

  const handleSave = (updatedForm: Record<string, string>) => {
    const nextData: UserAccountDetails = {
      ...accountData,
      email: updatedForm.opsEmail || accountData.email,
      commandLine: updatedForm.commandLine || accountData.commandLine,
      stationSector: updatedForm.stationSector || accountData.stationSector,
      dispatchId: updatedForm.dispatchId || accountData.dispatchId,
      commsFrequency: updatedForm.commsFrequency || accountData.commsFrequency,
      clearanceCert: updatedForm.clearanceCert || accountData.clearanceCert,
    };
    setAccountData(nextData);
    if (onUpdate) onUpdate(nextData);
  };

  return (
    <>
      <div className="rounded-xl border border-slate-800 bg-slate-900 p-6 shadow-sm">
        <div className="flex items-center justify-between border-b border-slate-800 pb-4 mb-5">
          <h4 className="text-base font-semibold text-slate-100">
            System & Operational Details
          </h4>
          <button
            onClick={() => setIsModalOpen(true)}
            className="flex items-center gap-1.5 px-3 py-1.5 text-sm font-medium text-slate-300 bg-slate-800 border border-slate-700 rounded-lg hover:bg-slate-700 transition-colors"
          >
            <PencilIcon className="h-4 w-4 text-slate-400" />
            <span>Edit</span>
          </button>
        </div>

        <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
          <div>
            <p className="text-xs font-medium text-slate-400">Email</p>
            <p className="mt-1 text-sm font-medium text-slate-100">{accountData.email}</p>
          </div>

          <div>
            <p className="text-xs font-medium text-slate-400">Hotline / Direct Line</p>
            <p className="mt-1 text-sm font-medium text-slate-100">{accountData.commandLine}</p>
          </div>

          <div>
            <p className="text-xs font-medium text-slate-400">Airspace / Station</p>
            <p className="mt-1 text-sm font-medium text-slate-100">{accountData.stationSector}</p>
          </div>

          <div>
            <p className="text-xs font-medium text-slate-400">Dispatch Unit ID</p>
            <p className="mt-1 text-sm font-medium text-slate-100">{accountData.dispatchId}</p>
          </div>

          <div>
            <p className="text-xs font-medium text-slate-400">Radio Frequency</p>
            <p className="mt-1 text-sm font-medium text-slate-100">{accountData.commsFrequency}</p>
          </div>

          <div>
            <p className="text-xs font-medium text-slate-400">Admin Clearance ID</p>
            <p className="mt-1 text-sm font-medium text-slate-100">{accountData.clearanceCert}</p>
          </div>
        </div>
      </div>

      <EditProfileModal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSave={handleSave}
        type="personal"
        initialData={{
          opsEmail: accountData.email,
          commandLine: accountData.commandLine,
          stationSector: accountData.stationSector,
          dispatchId: accountData.dispatchId,
          commsFrequency: accountData.commsFrequency,
          clearanceCert: accountData.clearanceCert,
        }}
      />
    </>
  );
}