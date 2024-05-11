import { userPermissionsService } from '../../../services/auth/user-permissions.service';
import Profile from '../../../shared/components/profile/profile.component';

const EmployeeProfile = () => {
  // TODO: replace with enum later
  const canEdit = userPermissionsService.hasOneOfPermissions(['all', 'users_update'])

  return <Profile canEdit={canEdit} isOwnProfile={false}/>;
};

export default EmployeeProfile;
