import { STORAGE_KEYS } from '../../shared/keys/storage-keys';
import { PermissionsEnum } from '../role/role.types';

class UserPermissionsService {
  hasPermission(permission: PermissionsEnum): boolean {
    const trueValue = 'true';
    const user = this.getUserFromJwt();
    return user[PermissionsEnum.all] === trueValue || user[permission] === trueValue;
  }

  private getUserFromJwt(): any {
    const token = localStorage.getItem(STORAGE_KEYS.JWT_TOKEN) ?? '';
    return JSON.parse(atob(token.split('.')[1]));
  }
}

export const userPermissionsService = new UserPermissionsService();
