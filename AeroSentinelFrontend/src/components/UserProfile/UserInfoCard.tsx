import { useState } from "react";
import EditProfileModal from "./Modals/EditProfileModal";
import { PencilIcon } from "../../icons";

export interface UserHeaderData {
  name: string;
  role: string;
  organization: string;
  status: string;
  clearanceLevel: string;
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
      name: "Musharof Chowdhury",
      role: "Operations Officer",
      organization: "Aircraft Control Center",
      status: "Active Duty",
      clearanceLevel: "Level 4 Clearance",
      avatarUrl: "/images/user/admin.jpg",
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
      <div className="relative overflow-hidden rounded-lg border border-gray-200 bg-white">
        {/* Cover Banner */}
        <div className="relative h-24 w-full bg-gray-50 sm:h-32 border-b border-gray-200">
          <div className="absolute top-4 right-4 flex items-center gap-1.5 rounded-md bg-white px-2.5 py-1 text-xs font-medium text-gray-700 border border-gray-200 shadow-sm">
            <span className="h-1.5 w-1.5 rounded-full bg-emerald-500" />
            <span className="tracking-wide uppercase text-[10px]">{headerData.status}</span>
          </div>
        </div>

        {/* Profile Card Main Body */}
        <div className="px-5 pb-6 pt-0 lg:px-7">
          <div className="flex flex-col sm:flex-row sm:items-end sm:justify-between -mt-10 sm:-mt-12 mb-4 gap-4">
            {/* Avatar container */}
            <div className="relative group">
              <div className="relative h-20 w-20 sm:h-24 sm:w-24 overflow-hidden rounded-lg border-4 border-white bg-white shadow-sm">
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
            </div>

            {/* Primary Action Button */}
            <button
              onClick={() => setIsModalOpen(true)}
              className="inline-flex items-center justify-center gap-2 rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-gray-200 sm:w-auto"
            >
              <PencilIcon className="h-4 w-4 text-gray-500" />
              <span>Edit Profile</span>
            </button>
          </div>

          {/* User Details */}
          <div className="space-y-4">
            <div>
              <div className="flex flex-wrap items-center gap-2 mb-1">
                <h3 className="text-xl font-semibold text-gray-900 tracking-tight">
                  {headerData.name}
                </h3>
                <span className="inline-flex items-center rounded-md bg-gray-100 px-2 py-0.5 text-xs font-medium text-gray-600 border border-gray-200">
                  {headerData.clearanceLevel}
                </span>
              </div>

              <p className="text-sm text-gray-600 flex items-center gap-2">
                <span>{headerData.role}</span>
                <span className="text-gray-300">•</span>
                <span className="text-gray-900">{headerData.organization}</span>
              </p>
            </div>

            {/* Quick System Stats */}
            <div className="flex flex-wrap gap-6 pt-4 border-t border-gray-100 text-sm text-gray-600">
              <div className="flex items-center gap-2">
                <span className="font-medium text-gray-500">Station</span>
                <span className="text-gray-900">BASE ALPHA-1</span>
              </div>
              <div className="flex items-center gap-2">
                <span className="font-medium text-gray-500">System Role</span>
                <span className="text-gray-900">OPERATIONS OFFICER</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Reusable Edit Modal */}
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