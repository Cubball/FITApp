import ErrorIcon from '../assets/icons/error-icon.svg';
import { PropsWithChildren } from 'react';
import { userPermissionsService } from '../services/auth/user-permissions.service';
import { PermissionsEnum } from '../services/role/role.types';

interface ProtectedRouteProps extends PropsWithChildren {
  permission: PermissionsEnum;
}

const ProtectedRoute = ({ permission, children }: ProtectedRouteProps) => {
  if (userPermissionsService.hasPermission(permission)) {
    return children;
  }

  return (
    <div className="flex w-full items-center justify-center gap-3 p-10 text-xl">
      <img src={ErrorIcon} className="h-10 w-10" />
      Ви не маєте доступу до даної сторінки
    </div>
  );
};

export default ProtectedRoute;
