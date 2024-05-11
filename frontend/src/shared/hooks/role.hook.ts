import { useMutation, useQuery } from 'react-query';
import { roleService } from '../../services/role/role.service';
import { ICreateRoleRequest, IRole } from '../../services/role/role.types';
import { QUERY_KEYS } from '../keys/query-keys';

interface UseRoleReturn {
  roles: Array<IRole> | undefined;
  handleCreateRole: (data: ICreateRoleRequest) => Promise<void>;
  handleUpdateRole: (id: string, data: ICreateRoleRequest) => Promise<void>;
  handleDeleteRole: (id: string) => Promise<void>;
  isGetRolesLoading: boolean;
  isCreateRoleLoading: boolean;
  isUpdateRoleLoading: boolean;
  isDeleteRoleLoading: boolean;
}

export const useRole = (): UseRoleReturn => {
  const { data: roles, isLoading: isGetRolesLoading } = useQuery([QUERY_KEYS.ROLES], () =>
    roleService.getRoles()
  );

  const { mutateAsync: mutateCreateRole, isLoading: isCreateRoleLoading } = useMutation(
    [QUERY_KEYS.MUTATE_ROLE],
    (data: ICreateRoleRequest) => roleService.createRole(data)
  );

  const { mutateAsync: mutateUpdateRole, isLoading: isUpdateRoleLoading } = useMutation(
    [QUERY_KEYS.MUTATE_ROLE],
    ({ id, data }: { id: string; data: ICreateRoleRequest }) => roleService.updateRole(id, data)
  );

  const { mutateAsync: mutateDeleteRole, isLoading: isDeleteRoleLoading } = useMutation(
    [QUERY_KEYS.MUTATE_ROLE],
    (id: string) => roleService.deleteRole(id)
  );

  const handleUpdateRole = async (id: string, data: ICreateRoleRequest) => {
    await mutateUpdateRole({ id, data });
  };

  const handleCreateRole = async (data: ICreateRoleRequest) => {
    await mutateCreateRole(data);
  };

  const handleDeleteRole = async (id: string) => {
    await mutateDeleteRole(id);
  };

  return {
    roles,
    handleCreateRole,
    handleUpdateRole,
    handleDeleteRole,
    isGetRolesLoading,
    isCreateRoleLoading,
    isUpdateRoleLoading,
    isDeleteRoleLoading
  };
};
