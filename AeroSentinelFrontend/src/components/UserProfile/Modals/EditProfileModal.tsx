import { useState, useEffect } from "react";
import { Modal } from "../../ui/modal";
import Button from "../../ui/button/Button";
import Input from "../../form/input/InputField";
import Label from "../../form/Label";

interface EditProfileModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (data: any) => void;
  initialData: {
    firstName: string;
    email: string;
    phone: string;
  };
}

export default function EditProfileModal({ isOpen, onClose, onSave, initialData }: EditProfileModalProps) {
  const [isAnimating, setIsAnimating] = useState(false);
  const [formData, setFormData] = useState(initialData);

  // Trigger the entrance animation slightly after the component mounts
  useEffect(() => {
    if (isOpen) {
      setFormData(initialData); // Reset to latest data when opened
      const timer = setTimeout(() => setIsAnimating(true), 10);
      return () => clearTimeout(timer);
    } else {
      setIsAnimating(false);
    }
  }, [isOpen, initialData]);

  // Handle the exit animation before unmounting
  const handleClose = () => {
    setIsAnimating(false);
    setTimeout(() => {
      onClose();
    }, 300); // 300ms matches the Tailwind transition duration
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(formData);
    handleClose();
  };

  return (
    <Modal 
      isOpen={isOpen} 
      onClose={handleClose} 
      className={`max-w-[700px] m-4 !bg-black/30 transition-all duration-300 ease-out ${
        isAnimating ? "opacity-100" : "opacity-0"
      }`}
    >
      <div 
        className={`relative w-full max-w-[700px] overflow-hidden rounded-3xl bg-white shadow-2xl dark:bg-gray-900 border border-gray-100 dark:border-gray-800 transition-all duration-300 ease-out transform ${
          isAnimating 
            ? "opacity-100 scale-100 translate-y-0" 
            : "opacity-0 scale-95 translate-y-4"
        }`}
      >
        <div className="px-6 py-5 text-white bg-gradient-to-r from-brand-500 to-indigo-600">
          <h4 className="text-xl font-bold tracking-tight">
            Edit Personal Information
          </h4>
          <p className="mt-1 text-xs text-brand-100">
            Update your account credentials and system profile details.
          </p>
        </div>

        <form onSubmit={handleSubmit} className="flex flex-col p-6 lg:p-8">
          <div className="custom-scrollbar max-h-[400px] overflow-y-auto pr-2 space-y-6">
            <div>
              <h5 className="mb-4 text-sm font-semibold tracking-wider text-gray-400 uppercase dark:text-gray-500">
                General Details
              </h5>
              <div className="grid grid-cols-1 gap-x-6 gap-y-4 lg:grid-cols-2">
                <div>
                  <Label className="font-medium text-gray-700 dark:text-gray-300">First Name</Label>
                  <Input 
                    type="text" 
                    name="firstName" 
                    value={formData.firstName} 
                    onChange={handleChange} 
                  />
                </div>
                
                <div>
                  <Label className="font-medium text-gray-700 dark:text-gray-300">Email Address</Label>
                  <Input 
                    type="text" 
                    name="email" 
                    value={formData.email} 
                    onChange={handleChange} 
                  />
                </div>

                <div>
                  <Label className="font-medium text-gray-700 dark:text-gray-300">Phone</Label>
                  <Input 
                    type="text" 
                    name="phone" 
                    value={formData.phone} 
                    onChange={handleChange} 
                  />
                </div>
              </div>
            </div>
          </div>
          
          <div className="flex items-center justify-end gap-3 pt-6 mt-6 border-t border-gray-100 dark:border-gray-800">
            <Button type="button" size="sm" variant="outline" onClick={handleClose} className="transition-transform rounded-xl active:scale-95">
              Cancel
            </Button>
            <Button type="submit" size="sm" className="text-white transition-transform shadow-sm rounded-xl bg-brand-500 hover:bg-brand-600 active:scale-95">
              Save Changes
            </Button>
          </div>
        </form>
      </div>
    </Modal>
  );
}