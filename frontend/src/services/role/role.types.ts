export enum PermissionsEnum {
  all = 'all',
  usersCreate = 'users_create',
  usersRead = 'users_read',
  usersUpdate = 'users_update',
  usersDelete = 'users_delete',
  rolesCreate = 'roles_create',
  rolesRead = 'roles_read',
  rolesUpdate = 'roles_update',
  rolesDelete = 'roles_delete'
}

export interface IRoleShortInfo {
  id: string;
  name: string;
}

export interface IRole extends IRoleShortInfo {
  permissions: string[];
}

export interface ICreateRoleRequest {
  name: string;
  permissions: PermissionsEnum[];
}

export interface IPermission {
  name: PermissionsEnum;
  description: string;
}
