import { useState } from "react";
import { useModal } from "../../hooks/useModal";
import EditProfileModal from "./Modals/EditProfileModal"; // Adjust path as needed

export default function UserMetaCard() {
  const { isOpen, openModal, closeModal } = useModal();
  
  // State lifted up to reflect changes on the card immediately after saving
  const [userData, setUserData] = useState({
    firstName: "Admin",
    email: "example@email.com",
    phone: "+63 917 123 4567"
  });

  const handleSave = (updatedData: typeof userData) => {
    console.log("Saving changes...", updatedData);
    setUserData(updatedData);
  };

  return (
    <>
      <div className="p-5 transition-all duration-300 border border-gray-200 rounded-2xl dark:border-gray-800 lg:p-6 hover:shadow-lg dark:hover:border-gray-700">
        <div className="flex flex-col gap-5 xl:flex-row xl:items-center xl:justify-between">
          <div className="flex flex-col items-center w-full gap-6 xl:flex-row">
            <div className="relative w-20 h-20 overflow-hidden border-2 rounded-full shadow-md border-brand-500">
              <img 
                src="/images/user/admin.jpg" 
                alt="user profile" 
                className="object-cover w-full h-full" 
              />
            </div>
            
            <div>
              <h4 className="mb-2 text-lg font-semibold text-center text-gray-800 dark:text-white/90 xl:text-left">
                {userData.firstName} 
              </h4>
              <div className="flex flex-col items-center gap-1 text-center xl:flex-row xl:gap-3 xl:text-left">
                <p className="text-sm font-medium text-gray-500 dark:text-gray-400">
                  Operations Officer
                </p>
                <div className="hidden h-3.5 w-px bg-gray-300 dark:bg-gray-700 xl:block"></div>
                <p className="text-sm text-gray-500 dark:text-gray-400">
                  Aircraft Control Center
                </p>
              </div>
            </div>
          </div>
          
          <button
            onClick={openModal}
            className="flex w-full items-center justify-center gap-2 rounded-xl bg-brand-500 px-5 py-3 text-sm font-medium text-white shadow-md transition-all duration-300 hover:bg-brand-600 hover:-translate-y-0.5 active:translate-y-0 active:scale-95 lg:inline-flex lg:w-auto"
          >
            <svg className="fill-current" width="18" height="18" viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path fillRule="evenodd" clipRule="evenodd" d="M15.0911 2.78206C14.2125 1.90338 12.7878 1.90338 11.9092 2.78206L4.57524 10.116C4.26682 10.4244 4.0547 10.8158 3.96468 11.2426L3.31231 14.3352C3.25997 14.5833 3.33653 14.841 3.51583 15.0203C3.69512 15.1996 3.95286 15.2761 4.20096 15.2238L7.29355 14.5714C7.72031 14.4814 8.11172 14.2693 8.42013 13.9609L15.7541 6.62695C16.6327 5.74827 16.6327 4.32365 15.7541 3.44497L15.0911 2.78206ZM12.9698 3.84272C13.2627 3.54982 13.7376 3.54982 14.0305 3.84272L14.6934 4.50563C14.9863 4.79852 14.9863 5.2734 14.6934 5.56629L14.044 6.21573L12.3204 4.49215L12.9698 3.84272ZM11.2597 5.55281L5.6359 11.1766C5.53309 11.2794 5.46238 11.4099 5.43238 11.5522L5.01758 13.5185L6.98394 13.1037C7.1262 13.0737 7.25666 13.003 7.35947 12.9002L12.9833 7.27639L11.2597 5.55281Z" fill=""/>
            </svg>
            Edit Profile
          </button>
        </div>
      </div>

      <EditProfileModal 
        isOpen={isOpen} 
        onClose={closeModal} 
        onSave={handleSave} 
        initialData={userData} 
      />
    </>
  );
}