import { PropsWithChildren } from 'react';
import { userPermissionsService } from '../services/auth/user-permissions.service';
import { PermissionsEnum } from '../services/role/role.types';

interface ProtectedRouteProps extends PropsWithChildren {
  permission: PermissionsEnum;
}

const ProtectedRoute = ({ permission, children }: ProtectedRouteProps) => {
  if (!userPermissionsService.hasPermission(permission)) {
    return <h1>DENIED</h1>;
  }

  return children;
};

export default ProtectedRoute;
