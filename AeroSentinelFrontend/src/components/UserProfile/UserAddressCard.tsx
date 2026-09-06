import { useState, useEffect } from "react";
import { useModal } from "../../hooks/useModal";
import { Modal } from "../ui/modal";
import Button from "../ui/button/Button";
import Input from "../form/input/InputField";
import Label from "../form/Label";

export default function UserAddressCard() {
  const { isOpen, openModal, closeModal } = useModal();
  const [isAnimating, setIsAnimating] = useState(false);

  const [formData, setFormData] = useState({
    country: "United States",
    cityState: "Phoenix, Arizona, United States",
    postalCode: "ERT 2489",
    taxId: "AS4568384"
  });

  useEffect(() => {
    if (isOpen) {
      const timer = setTimeout(() => setIsAnimating(true), 10);
      return () => clearTimeout(timer);
    }
  }, [isOpen]);

  const handleClose = () => {
    setIsAnimating(false);
    setTimeout(() => {
      closeModal();
    }, 10);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSave = () => {
    console.log("Saving address changes...", formData);
    handleClose();
  };

  return (
    <>
      <div className="p-5 transition-all duration-300 border border-gray-200 rounded-2xl dark:border-gray-800 lg:p-6 hover:shadow-lg dark:hover:border-gray-700">
        <div className="flex flex-row items-center justify-between mb-6">
          <h4 className="text-lg font-semibold text-gray-800 dark:text-white/90">
            Address
          </h4>
          <button
            onClick={openModal}
            className="flex items-center gap-2 px-4 py-2 text-sm font-medium text-gray-700 transition-colors bg-transparent border border-gray-300 rounded-full hover:bg-gray-50 dark:border-gray-600 dark:text-gray-300 dark:hover:bg-gray-800 dark:hover:text-white"
          >
            <svg className="fill-current" width="14" height="14" viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path fillRule="evenodd" clipRule="evenodd" d="M15.0911 2.78206C14.2125 1.90338 12.7878 1.90338 11.9092 2.78206L4.57524 10.116C4.26682 10.4244 4.0547 10.8158 3.96468 11.2426L3.31231 14.3352C3.25997 14.5833 3.33653 14.841 3.51583 15.0203C3.69512 15.1996 3.95286 15.2761 4.20096 15.2238L7.29355 14.5714C7.72031 14.4814 8.11172 14.2693 8.42013 13.9609L15.7541 6.62695C16.6327 5.74827 16.6327 4.32365 15.7541 3.44497L15.0911 2.78206ZM12.9698 3.84272C13.2627 3.54982 13.7376 3.54982 14.0305 3.84272L14.6934 4.50563C14.9863 4.79852 14.9863 5.2734 14.6934 5.56629L14.044 6.21573L12.3204 4.49215L12.9698 3.84272ZM11.2597 5.55281L5.6359 11.1766C5.53309 11.2794 5.46238 11.4099 5.43238 11.5522L5.01758 13.5185L6.98394 13.1037C7.1262 13.0737 7.25666 13.003 7.35947 12.9002L12.9833 7.27639L11.2597 5.55281Z" fill=""/>
            </svg>
            Edit
          </button>
        </div>

        <div className="grid grid-cols-1 gap-4 lg:grid-cols-2 lg:gap-7 2xl:gap-x-32">
          <div>
            <p className="mb-1 text-xs font-medium text-gray-500 uppercase dark:text-gray-400">
              Country
            </p>
            <p className="text-sm font-medium text-gray-900 dark:text-white/90">
              {formData.country}
            </p>
          </div>

          <div>
            <p className="mb-1 text-xs font-medium text-gray-500 uppercase dark:text-gray-400">
              City/State
            </p>
            <p className="text-sm font-medium text-gray-900 dark:text-white/90">
              {formData.cityState}
            </p>
          </div>

          <div>
            <p className="mb-1 text-xs font-medium text-gray-500 uppercase dark:text-gray-400">
              Postal Code
            </p>
            <p className="text-sm font-medium text-gray-900 dark:text-white/90">
              {formData.postalCode}
            </p>
          </div>

          <div>
            <p className="mb-1 text-xs font-medium text-gray-500 uppercase dark:text-gray-400">
              TAX ID
            </p>
            <p className="text-sm font-medium text-gray-900 dark:text-white/90">
              {formData.taxId}
            </p>
          </div>
        </div>
      </div>

      <Modal 
        isOpen={isOpen} 
        onClose={handleClose} 
        className={`max-w-[700px] m-4 !bg-black/30 backdrop-blur-sm transition-all duration-300 ease-out ${
          isAnimating ? "opacity-100" : "opacity-0 pointer-events-none"
        }`}
      >
        <div 
          className={`relative w-full max-w-[700px] overflow-hidden rounded-3xl bg-white shadow-2xl dark:bg-gray-900 border border-gray-100 dark:border-gray-800 transition-all duration-300 ease-out transform ${
            isAnimating ? "opacity-100 scale-100 translate-y-0" : "opacity-0 scale-95 translate-y-4"
          }`}
        >
          <div className="px-6 py-5 text-white bg-gradient-to-r from-brand-500 to-indigo-600">
            <h4 className="text-xl font-bold tracking-tight">Edit Address</h4>
            <p className="mt-1 text-xs text-brand-100">Update your PRIME-Air facility location details.</p>
          </div>

          <form className="flex flex-col p-6 lg:p-8">
            <div className="grid grid-cols-1 gap-x-6 gap-y-5 lg:grid-cols-2">
              <div>
                <Label className="font-medium text-gray-700 dark:text-gray-300">Country</Label>
                <Input type="text" name="country" value={formData.country} onChange={handleChange} />
              </div>

              <div>
                <Label className="font-medium text-gray-700 dark:text-gray-300">City/State</Label>
                <Input type="text" name="cityState" value={formData.cityState} onChange={handleChange} />
              </div>

              <div>
                <Label className="font-medium text-gray-700 dark:text-gray-300">Postal Code</Label>
                <Input type="text" name="postalCode" value={formData.postalCode} onChange={handleChange} />
              </div>

              <div>
                <Label className="font-medium text-gray-700 dark:text-gray-300">TAX ID</Label>
                <Input type="text" name="taxId" value={formData.taxId} onChange={handleChange} />
              </div>
            </div>

            <div className="flex items-center justify-end gap-3 pt-6 mt-6 border-t border-gray-100 dark:border-gray-800">
              <Button size="sm" variant="outline" onClick={handleClose} className="transition-transform rounded-xl active:scale-95">
                Cancel
              </Button>
              <Button size="sm" onClick={handleSave} className="text-white transition-transform shadow-sm rounded-xl bg-brand-500 hover:bg-brand-600 active:scale-95">
                Save Changes
              </Button>
            </div>
          </form>
        </div>
      </Modal>
    </>
  );
}