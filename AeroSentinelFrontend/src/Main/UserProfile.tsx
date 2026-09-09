import PageBreadcrumb from "../components/common/PageBreadCrumb";
import PageMeta from "../components/common/PageMeta";
import UserProfileCard from "../components/UserProfile/UserProfileCard";

export default function UserProfile() {
  return (
    <>
      <PageMeta
        title="User Profile | AeroSentinel Flight Operations Center"
        description="User Profile and Staff Information Dashboard for AeroSentinel Real-Time Flight Message Integrity Evaluation"
      />
      <PageBreadcrumb pageTitle="User Profile" />
      <div className="mt-4">
        <UserProfileCard />
      </div>
    </>
  );
}
