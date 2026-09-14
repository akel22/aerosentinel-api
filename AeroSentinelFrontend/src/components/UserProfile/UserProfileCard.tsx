import UserInfoCard from "./UserInfoCard";
import UserAccountCard from "./UserAccountCard";

export default function UserProfileCard() {
  return (
    <div className="mx-auto max-w-4xl space-y-6">
      <UserInfoCard />
      <UserAccountCard />
    </div>
  );
}