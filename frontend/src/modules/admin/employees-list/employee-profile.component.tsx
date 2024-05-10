import Profile from '../../../shared/components/profile/profile.component';

const EmployeeProfile = () => {
  const canEdit = true; // TODO: get permissions and determine whether admin can edit employees

  return <Profile canEdit={canEdit} isOwnProfile={false}/>;
};

export default EmployeeProfile;
