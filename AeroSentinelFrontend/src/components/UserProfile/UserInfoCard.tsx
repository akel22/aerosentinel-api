import { useState } from "react";
import EditProfileModal from "./Modals/EditProfileModal";
import { PencilIcon } from "../../icons";

export interface UserHeaderData {
  name: string;
  role: string;
  organization: string;
  avatarUrl: string;
}

interface UserInfoCardProps {
  data?: UserHeaderData;
  onUpdate?: (updated: UserHeaderData) => void;
}

export default function UserInfoCard({ data, onUpdate }: UserInfoCardProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [headerData, setHeaderData] = useState<UserHeaderData>(
    data || {
      name: "Administrator",
      role: "Operations Officer",
      organization: "Aircraft Control Center",

      avatarUrl: "",
    }
  );

  const handleSave = (updatedForm: Record<string, string>) => {
    const nextData: UserHeaderData = {
      ...headerData,
      name: updatedForm.name || headerData.name,
      role: updatedForm.role || headerData.role,
      organization: updatedForm.organization || headerData.organization,
    };
    setHeaderData(nextData);
    if (onUpdate) onUpdate(nextData);
  };

  return (
    <>
      <div className="rounded-xl border border-slate-800 bg-slate-900 p-6 shadow-sm">
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
          <div className="flex items-center gap-4">
            <div className="relative h-16 w-16 shrink-0 overflow-hidden rounded-full border border-slate-700">
              <img
                src={headerData.avatarUrl}
                alt={headerData.name}
                className="h-full w-full object-cover"
                onError={(e) => {
                  (e.target as HTMLImageElement).src =
                    "https://images.unsplash.com/photo-1534528741775-53994a69daeb?auto=format&fit=crop&w=300&q=80";
                }}
              />
            </div>

            <div>
              <div className="flex items-center gap-2">
                <h3 className="text-lg font-semibold text-slate-100">{headerData.name}</h3>          
              </div>
              <p className="text-sm text-slate-400">
                {headerData.role} • {headerData.organization}
              </p>
            </div>
          </div>

          <button
            onClick={() => setIsModalOpen(true)}
            className="inline-flex items-center justify-center gap-1.5 rounded-lg border border-slate-700 bg-slate-800 px-3.5 py-2 text-sm font-medium text-slate-200 shadow-sm hover:bg-slate-700 transition-colors"
          >
            <PencilIcon className="h-4 w-4 text-slate-400" />
            <span>Edit Profile</span>
          </button>
        </div>
      </div>

      <EditProfileModal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSave={handleSave}
        type="info"
        initialData={{
          name: headerData.name,
          role: headerData.role,
          organization: headerData.organization,
        }}
      />
    </>
  );
}