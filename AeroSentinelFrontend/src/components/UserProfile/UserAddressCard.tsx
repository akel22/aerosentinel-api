import { useState } from "react";
import EditProfileModal from "./Modals/EditProfileModal";
import { PencilIcon, CopyIcon, CheckLineIcon } from "../../icons";

export interface UserAddressMeta {
  country: string;
  cityState: string;
  postalCode: string;
  taxId: string;
}

interface UserAddressCardProps {
  data?: UserAddressMeta;
  onUpdate?: (updated: UserAddressMeta) => void;
}

export default function UserAddressCard({ data, onUpdate }: UserAddressCardProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [copiedField, setCopiedField] = useState<string | null>(null);

  const [addressData, setAddressData] = useState<UserAddressMeta>(
    data || {
      country: "United States",
      cityState: "Phoenix, Arizona, United States",
      postalCode: "ERT 2489",
      taxId: "AS45658384",
    }
  );

  const handleCopy = (text: string, fieldName: string) => {
    navigator.clipboard.writeText(text);
    setCopiedField(fieldName);
    setTimeout(() => setCopiedField(null), 2000);
  };

  const handleSave = (updatedForm: Record<string, string>) => {
    const nextData: UserAddressMeta = {
      ...addressData,
      country: updatedForm.country || addressData.country,
      cityState: updatedForm.cityState || addressData.cityState,
      postalCode: updatedForm.postalCode || addressData.postalCode,
      taxId: updatedForm.taxId || addressData.taxId,
    };
    setAddressData(nextData);
    if (onUpdate) onUpdate(nextData);
  };

  return (
    <>
      <div className="rounded-lg border border-gray-200 bg-white p-6">
        {/* Card Header */}
        <div className="flex items-center justify-between mb-6 pb-4 border-b border-gray-200">
          <div>
            <h4 className="text-base font-semibold text-gray-900">
              Facility Address & Tax ID
            </h4>
            <p className="text-sm text-gray-500 mt-1">
              Registered PRIME-Air station location and tax credentials
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

        {/* Address Grid */}
        <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
          {/* Country */}
          <div>
            <p className="text-xs font-medium text-gray-500 mb-1">
              Country / Jurisdiction
            </p>
            <p className="text-sm text-gray-900 flex items-center gap-2">
              <span>{addressData.country}</span>
              <span className="rounded bg-gray-100 px-1.5 py-0.5 text-[10px] font-medium text-gray-600 border border-gray-200">
                VERIFIED
              </span>
            </p>
          </div>

          {/* City/State */}
          <div>
            <p className="text-xs font-medium text-gray-500 mb-1">
              City / State
            </p>
            <p className="text-sm text-gray-900">
              {addressData.cityState}
            </p>
          </div>

          {/* Postal Code */}
          <div>
            <p className="text-xs font-medium text-gray-500 mb-1">
              Postal Code
            </p>
            <p className="text-sm text-gray-900">
              {addressData.postalCode}
            </p>
          </div>

          {/* Tax ID */}
          <div className="relative group">
            <div className="flex items-center gap-2 mb-1">
              <p className="text-xs font-medium text-gray-500">
                TAX ID / Facility Reg
              </p>
              <button
                onClick={() => handleCopy(addressData.taxId, "taxId")}
                className="text-gray-400 hover:text-gray-600"
                title="Copy Tax ID"
              >
                {copiedField === "taxId" ? (
                  <CheckLineIcon className="h-3.5 w-3.5 text-gray-600" />
                ) : (
                  <CopyIcon className="h-3.5 w-3.5" />
                )}
              </button>
            </div>
            <p className="text-sm text-gray-900">
              {addressData.taxId}
            </p>
          </div>
        </div>
      </div>

      {/* Reusable Edit Modal */}
      <EditProfileModal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSave={handleSave}
        type="address"
        initialData={{
          country: addressData.country,
          cityState: addressData.cityState,
          postalCode: addressData.postalCode,
          taxId: addressData.taxId,
        }}
      />
    </>
  );
}