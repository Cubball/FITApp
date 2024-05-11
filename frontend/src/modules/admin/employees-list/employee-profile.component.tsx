import { userPermissionsService } from '../../../services/auth/user-permissions.service';
import { PermissionsEnum } from '../../../services/role/role.types';
import Profile from '../../../shared/components/profile/profile.component';

const EmployeeProfile = () => {
  const canEdit = userPermissionsService.hasPermission(PermissionsEnum.usersUpdate)
  return <Profile canEdit={canEdit} isOwnProfile={false}/>;
};

export default EmployeeProfile;
