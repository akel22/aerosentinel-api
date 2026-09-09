import { useState } from "react";
import EditProfileModal from "./Modals/EditProfileModal";
import { PencilIcon, CopyIcon, CheckLineIcon } from "../../icons";

export interface UserPersonalMeta {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  bio: string;
}

interface UserMetaCardProps {
  data?: UserPersonalMeta;
  onUpdate?: (updated: UserPersonalMeta) => void;
}

export default function UserMetaCard({ data, onUpdate }: UserMetaCardProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [copiedField, setCopiedField] = useState<string | null>(null);

  const [personalData, setPersonalData] = useState<UserPersonalMeta>(
    data || {
      firstName: "Musharof",
      lastName: "Chowdhury",
      email: "randomuser@pimjo.com",
      phone: "+09 363 398 46",
      bio: "Team Manager & Lead Aircraft Operations Controller.",
    }
  );

  const handleCopy = (text: string, fieldName: string) => {
    navigator.clipboard.writeText(text);
    setCopiedField(fieldName);
    setTimeout(() => setCopiedField(null), 2000);
  };

  const handleSave = (updatedForm: Record<string, string>) => {
    const nextData: UserPersonalMeta = {
      ...personalData,
      firstName: updatedForm.firstName || personalData.firstName,
      lastName: updatedForm.lastName || personalData.lastName,
      email: updatedForm.email || personalData.email,
      phone: updatedForm.phone || personalData.phone,
      bio: updatedForm.bio || personalData.bio,
    };
    setPersonalData(nextData);
    if (onUpdate) onUpdate(nextData);
  };

  return (
    <>
      <div className="rounded-lg border border-gray-200 bg-white p-6">
        {/* Card Header */}
        <div className="flex items-center justify-between mb-6 pb-4 border-b border-gray-200">
          <div>
            <h4 className="text-base font-semibold text-gray-900">
              Personal Information
            </h4>
            <p className="text-sm text-gray-500 mt-1">
              Staff member bio and verified contact details
            </p>
          </div>

          <button
            onClick={() => setIsModalOpen(true)}
            className="flex items-center gap-1.5 px-3 py-1.5 text-sm font-medium text-gray-600 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50"
          >
            <PencilIcon className="h-4 w-4 text-gray-500" />
            <span>Edit</span>
          </button>
        </div>

        {/* Info Grid */}
        <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
          {/* First Name */}
          <div>
            <p className="text-xs font-medium text-gray-500 mb-1">
              First Name
            </p>
            <p className="text-sm text-gray-900">
              {personalData.firstName}
            </p>
          </div>

          {/* Last Name */}
          <div>
            <p className="text-xs font-medium text-gray-500 mb-1">
              Last Name
            </p>
            <p className="text-sm text-gray-900">
              {personalData.lastName}
            </p>
          </div>

          {/* Email */}
          <div className="relative group">
            <div className="flex items-center gap-2 mb-1">
              <p className="text-xs font-medium text-gray-500">
                Email Address
              </p>
              <button
                onClick={() => handleCopy(personalData.email, "email")}
                className="text-gray-400 hover:text-gray-600"
                title="Copy Email"
              >
                {copiedField === "email" ? (
                  <CheckLineIcon className="h-3.5 w-3.5 text-gray-600" />
                ) : (
                  <CopyIcon className="h-3.5 w-3.5" />
                )}
              </button>
            </div>
            <p className="text-sm text-gray-900">
              {personalData.email}
            </p>
          </div>

          {/* Phone */}
          <div className="relative group">
            <div className="flex items-center gap-2 mb-1">
              <p className="text-xs font-medium text-gray-500">
                Phone Number
              </p>
              <button
                onClick={() => handleCopy(personalData.phone, "phone")}
                className="text-gray-400 hover:text-gray-600"
                title="Copy Phone"
              >
                {copiedField === "phone" ? (
                  <CheckLineIcon className="h-3.5 w-3.5 text-gray-600" />
                ) : (
                  <CopyIcon className="h-3.5 w-3.5" />
                )}
              </button>
            </div>
            <p className="text-sm text-gray-900">
              {personalData.phone}
            </p>
          </div>

          {/* Bio */}
          <div className="sm:col-span-2">
            <p className="text-xs font-medium text-gray-500 mb-1">
              Bio / Duty Notes
            </p>
            <p className="text-sm text-gray-900">
              {personalData.bio}
            </p>
          </div>
        </div>
      </div>

      {/* Reusable Edit Modal */}
      <EditProfileModal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSave={handleSave}
        type="personal"
        initialData={{
          firstName: personalData.firstName,
          lastName: personalData.lastName,
          email: personalData.email,
          phone: personalData.phone,
          bio: personalData.bio,
        }}
      />
    </>
  );
}