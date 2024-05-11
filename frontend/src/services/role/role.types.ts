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

export interface IRole {
  id: string;
  neme: string;
}

export interface IRolesResponse {
  roles: Array<IRole>;
}

export interface ICreateRoleRequest {
  name: string;
  permissions: PermissionsEnum[];
}
