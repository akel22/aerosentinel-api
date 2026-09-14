import { useState, useEffect } from "react";
import { Modal } from "../../ui/modal";
import Button from "../../ui/button/Button";
import Input from "../../form/input/InputField";
import Label from "../../form/Label";
import { PencilIcon } from "../../../icons";

export type ModalType = "info" | "personal" | "account";

interface EditProfileModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (data: Record<string, string>) => void;
  type: ModalType;
  initialData: Record<string, string>;
}

export default function EditProfileModal({
  isOpen,
  onClose,
  onSave,
  type,
  initialData,
}: EditProfileModalProps) {
  const [isAnimating, setIsAnimating] = useState(false);
  const [formData, setFormData] = useState<Record<string, string>>(initialData);

  useEffect(() => {
    if (isOpen) {
      setFormData(initialData);
      const timer = setTimeout(() => setIsAnimating(true), 10);
      return () => clearTimeout(timer);
    } else {
      setIsAnimating(false);
    }
  }, [isOpen, initialData]);

  const handleClose = () => {
    setIsAnimating(false);
    setTimeout(() => {
      onClose();
    }, 200);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(formData);
    handleClose();
  };

  const getModalTitle = () => {
    switch (type) {
      case "info":
        return {
          title: "Edit Profile Header",
          subtitle: "Update administrator name, operational role, and station.",
        };
      case "personal":
      case "account":
        return {
          title: "Edit Operational Details",
          subtitle: "Update contact details, station assignment, radio frequency, and clearance ID.",
        };
    }
  };

  const { title, subtitle } = getModalTitle();

  return (
    <Modal
      isOpen={isOpen}
      onClose={handleClose}
      showCloseButton={true}
      className={`max-w-[640px] m-4 !bg-transparent shadow-none border-none transition-all duration-300 ease-out ${
        isAnimating ? "opacity-100 scale-100" : "opacity-0 scale-95"
      }`}
    >
      <div className="relative w-full overflow-hidden rounded-2xl bg-white dark:bg-gray-900 border border-gray-200 dark:border-gray-800 shadow-xl">
        {/* Solid Color Header Header */}
        <div className="px-6 py-5 text-white bg-slate-900 border-b border-slate-800">
          <div className="flex items-center gap-3">
            <div className="flex h-9 w-9 items-center justify-center rounded-lg bg-slate-800 border border-slate-700">
              <PencilIcon className="w-4 h-4 text-white" />
            </div>
            <div>
              <h4 className="text-lg font-semibold tracking-tight text-white">{title}</h4>
              <p className="text-xs text-slate-400 font-normal">{subtitle}</p>
            </div>
          </div>
        </div>

        {/* Form Body */}
        <form onSubmit={handleSubmit} className="p-6 flex flex-col">
          <div className="space-y-5 max-h-[460px] overflow-y-auto pr-1">
            {type === "info" && (
              <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                <div className="sm:col-span-2">
                  <Label className="font-medium text-xs text-gray-700 dark:text-gray-300">
                    Full Name
                  </Label>
                  <div className="mt-1">
                    <Input
                      type="text"
                      name="name"
                      value={formData.name || ""}
                      onChange={handleChange}
                      placeholder="e.g. Musharof Chowdhury"
                    />
                  </div>
                </div>

                <div>
                  <Label className="font-medium text-xs text-gray-700 dark:text-gray-300">
                    Operational Title
                  </Label>
                  <Input
                    type="text"
                    name="role"
                    value={formData.role || ""}
                    onChange={handleChange}
                    placeholder="e.g. Operations Officer"
                  />
                </div>

                <div>
                  <Label className="font-medium text-xs text-gray-700 dark:text-gray-300">
                    Station / Department
                  </Label>
                  <Input
                    type="text"
                    name="organization"
                    value={formData.organization || ""}
                    onChange={handleChange}
                    placeholder="e.g. Aircraft Control Center"
                  />
                </div>
              </div>
            )}

            {(type === "personal" || type === "account") && (
              <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                <div>
                  <Label className="font-medium text-xs text-gray-700 dark:text-gray-300">
                    Ops Email
                  </Label>
                  <Input
                    type="email"
                    name="opsEmail"
                    value={formData.opsEmail || ""}
                    onChange={handleChange}
                    placeholder="e.g. m.chowdhury@primeair.ph"
                  />
                </div>

                <div>
                  <Label className="font-medium text-xs text-gray-700 dark:text-gray-300">
                    Hotline / Direct Line
                  </Label>
                  <Input
                    type="text"
                    name="commandLine"
                    value={formData.commandLine || ""}
                    onChange={handleChange}
                    placeholder="e.g. +63 (2) 8876-1234"
                  />
                </div>

                <div>
                  <Label className="font-medium text-xs text-gray-700 dark:text-gray-300">
                    Airspace / Station
                  </Label>
                  <Input
                    type="text"
                    name="stationSector"
                    value={formData.stationSector || ""}
                    onChange={handleChange}
                    placeholder="e.g. Manila FIR (RPLL - NAIA Hub)"
                  />
                </div>

                <div>
                  <Label className="font-medium text-xs text-gray-700 dark:text-gray-300">
                    Dispatch Unit ID
                  </Label>
                  <Input
                    type="text"
                    name="dispatchId"
                    value={formData.dispatchId || ""}
                    onChange={handleChange}
                    placeholder="e.g. MNL-DISPATCH-01"
                  />
                </div>

                <div>
                  <Label className="font-medium text-xs text-gray-700 dark:text-gray-300">
                    Radio Frequency
                  </Label>
                  <Input
                    type="text"
                    name="commsFrequency"
                    value={formData.commsFrequency || ""}
                    onChange={handleChange}
                    placeholder="e.g. VHF 124.10 MHz (Manila Tower)"
                  />
                </div>

                <div>
                  <Label className="font-medium text-xs text-gray-700 dark:text-gray-300">
                    Admin Clearance ID
                  </Label>
                  <Input
                    type="text"
                    name="clearanceCert"
                    value={formData.clearanceCert || ""}
                    onChange={handleChange}
                    placeholder="e.g. CAAP Admin Badge #8841"
                  />
                </div>
              </div>
            )}
          </div>

          {/* Action Footer */}
          <div className="flex items-center justify-end gap-3 pt-5 mt-6 border-t border-gray-100 dark:border-gray-800">
            <Button
              type="button"
              size="sm"
              variant="outline"
              onClick={handleClose}
              className="rounded-lg border-gray-300 dark:border-gray-700 text-gray-700 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-800"
            >
              Cancel
            </Button>
            <Button
              type="submit"
              size="sm"
              className="rounded-lg bg-blue-600 hover:bg-blue-700 text-white font-medium px-4 shadow-none"
            >
              Save Changes
            </Button>
          </div>
        </form>
      </div>
    </Modal>
  );
}