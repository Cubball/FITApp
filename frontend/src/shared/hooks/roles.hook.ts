import { useMutation, useQuery } from 'react-query';
import { IRoleShortInfo } from '../../services/role/role.types';
import { QUERY_KEYS } from '../keys/query-keys';
import { roleService } from '../../services/role/role.service';

interface IUseRolesReturn {
  roles: Array<IRoleShortInfo> | undefined;
  handleDeleteRole: (id: string) => Promise<void>;
  isGetRolesLoading: boolean;
  isDeleteRoleLoading: boolean;
}

export const useRoles = (): IUseRolesReturn => {
  const { data: roles, isLoading: isGetRolesLoading } = useQuery([QUERY_KEYS.ROLES], () =>
    roleService.getRoles()
  );

  const { mutateAsync: mutateDeleteRole, isLoading: isDeleteRoleLoading } = useMutation(
    [QUERY_KEYS.MUTATE_ROLE],
    (id: string) => roleService.deleteRole(id)
  );

  const handleDeleteRole = async (id: string) => {
    await mutateDeleteRole(id);
  };

  return {
    roles,
    handleDeleteRole,
    isGetRolesLoading,
    isDeleteRoleLoading
  };
};
