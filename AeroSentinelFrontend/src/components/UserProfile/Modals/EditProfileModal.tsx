import { useState, useEffect } from "react";
import { Modal } from "../../ui/modal";
import Button from "../../ui/button/Button";
import Input from "../../form/input/InputField";
import Label from "../../form/Label";
import { PencilIcon } from "../../../icons";

export type ModalType = "info" | "personal" | "address";

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
          subtitle: "Update identity summary, officer title, and command post details.",
        };
      case "personal":
        return {
          title: "Edit Personal Details",
          subtitle: "Modify staff contact information and bio.",
        };
      case "address":
        return {
          title: "Edit Address & Station",
          subtitle: "Update facility assignment, location codes, and tax identification.",
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
      <div className="relative w-full overflow-hidden rounded-3xl bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800/80 shadow-2xl transition-all">
        {/* Header Banner */}
        <div className="relative px-6 py-6 text-white bg-gradient-to-r from-brand-600 via-indigo-600 to-slate-900 overflow-hidden">
          <div className="absolute -right-6 -bottom-6 w-32 h-32 bg-white/5 rounded-full pointer-events-none" />
          <div className="flex items-center gap-3">
            <div className="flex h-10 w-10 items-center justify-center rounded-xl bg-white/10 border border-white/20">
              <PencilIcon className="w-5 h-5 text-white" />
            </div>
            <div>
              <h4 className="text-xl font-bold tracking-tight text-white">{title}</h4>
              <p className="mt-0.5 text-xs text-blue-100/80 font-normal">{subtitle}</p>
            </div>
          </div>
        </div>

        {/* Form Body */}
        <form onSubmit={handleSubmit} className="p-6 lg:p-8 flex flex-col">
          <div className="space-y-5 max-h-[460px] overflow-y-auto pr-1">
            {type === "info" && (
              <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                <div className="sm:col-span-2">
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    Full Name
                  </Label>
                  <div className="relative mt-1">
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
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
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
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
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

            {type === "personal" && (
              <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                <div>
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    First Name
                  </Label>
                  <Input
                    type="text"
                    name="firstName"
                    value={formData.firstName || ""}
                    onChange={handleChange}
                  />
                </div>

                <div>
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    Last Name
                  </Label>
                  <Input
                    type="text"
                    name="lastName"
                    value={formData.lastName || ""}
                    onChange={handleChange}
                  />
                </div>

                <div>
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    Email Address
                  </Label>
                  <Input
                    type="email"
                    name="email"
                    value={formData.email || ""}
                    onChange={handleChange}
                  />
                </div>

                <div>
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    Phone Number
                  </Label>
                  <Input
                    type="text"
                    name="phone"
                    value={formData.phone || ""}
                    onChange={handleChange}
                  />
                </div>

                <div className="sm:col-span-2">
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    Bio / Assignment Summary
                  </Label>
                  <Input
                    type="text"
                    name="bio"
                    value={formData.bio || ""}
                    onChange={handleChange}
                  />
                </div>
              </div>
            )}

            {type === "address" && (
              <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                <div>
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    Country
                  </Label>
                  <Input
                    type="text"
                    name="country"
                    value={formData.country || ""}
                    onChange={handleChange}
                  />
                </div>

                <div>
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    City / State
                  </Label>
                  <Input
                    type="text"
                    name="cityState"
                    value={formData.cityState || ""}
                    onChange={handleChange}
                  />
                </div>

                <div>
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    Postal Code
                  </Label>
                  <Input
                    type="text"
                    name="postalCode"
                    value={formData.postalCode || ""}
                    onChange={handleChange}
                  />
                </div>

                <div>
                  <Label className="font-semibold text-xs uppercase tracking-wider text-gray-700 dark:text-gray-300">
                    Tax / Facility ID
                  </Label>
                  <Input
                    type="text"
                    name="taxId"
                    value={formData.taxId || ""}
                    onChange={handleChange}
                  />
                </div>
              </div>
            )}
          </div>

          {/* Action Footer */}
          <div className="flex items-center justify-end gap-3 pt-6 mt-6 border-t border-gray-100 dark:border-gray-800">
            <Button
              type="button"
              size="sm"
              variant="outline"
              onClick={handleClose}
              className="rounded-xl border-gray-300 dark:border-gray-700 text-gray-700 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-800 transition-all active:scale-95"
            >
              Cancel
            </Button>
            <Button
              type="submit"
              size="sm"
              className="rounded-xl bg-brand-500 hover:bg-brand-600 text-white shadow-md shadow-brand-500/25 transition-all active:scale-95 font-medium px-5"
            >
              Save Changes
            </Button>
          </div>
        </form>
      </div>
    </Modal>
  );
}