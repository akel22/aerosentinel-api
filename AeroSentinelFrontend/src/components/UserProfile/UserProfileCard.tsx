import UserInfoCard, { UserHeaderData } from "./UserInfoCard";
import UserMetaCard, { UserPersonalMeta } from "./UserMetaCard";
import UserAddressCard, { UserAddressMeta } from "./UserAddressCard";

interface UserProfileCardProps {
  headerData?: UserHeaderData;
  personalData?: UserPersonalMeta;
  addressData?: UserAddressMeta;
  onUpdateHeader?: (updated: UserHeaderData) => void;
  onUpdatePersonal?: (updated: UserPersonalMeta) => void;
  onUpdateAddress?: (updated: UserAddressMeta) => void;
}

export default function UserProfileCard({
  headerData,
  personalData,
  addressData,
  onUpdateHeader,
  onUpdatePersonal,
  onUpdateAddress,
}: UserProfileCardProps) {
  return (
    <div className="mx-auto max-w-6xl space-y-6">
      {/* 1. Header Profile Summary Card */}
      <UserInfoCard data={headerData} onUpdate={onUpdateHeader} />

      {/* 2. Personal Information Card */}
      <UserMetaCard data={personalData} onUpdate={onUpdatePersonal} />

      {/* 3. Address & Facility Details Card */}
      <UserAddressCard data={addressData} onUpdate={onUpdateAddress} />
    </div>
  );
}
