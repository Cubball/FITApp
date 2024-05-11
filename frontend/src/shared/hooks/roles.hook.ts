import { useMutation, useQuery, useQueryClient } from 'react-query';
import { IRoleShortInfo } from '../../services/role/role.types';
import { QUERY_KEYS } from '../keys/query-keys';
import { roleService } from '../../services/role/role.service';
import { createOnError } from '../helpers/toast.helpers';

interface IUseRolesReturn {
  roles: Array<IRoleShortInfo> | undefined;
  handleDeleteRole: (id: string) => Promise<void>;
  isGetRolesLoading: boolean;
  isDeleteRoleLoading: boolean;
}

export const useRoles = (): IUseRolesReturn => {
  const queryClient = useQueryClient();
  const { data: roles, isLoading: isGetRolesLoading } = useQuery([QUERY_KEYS.ROLES], () =>
    roleService.getRoles()
  );

  const { mutateAsync: mutateDeleteRole, isLoading: isDeleteRoleLoading } = useMutation({
    mutationKey: [QUERY_KEYS.MUTATE_ROLE],
    mutationFn: (id: string) => roleService.deleteRole(id),
    onSuccess: () => queryClient.invalidateQueries([QUERY_KEYS.ROLES]),
    onError: createOnError('Не вдалося видалити роль')
  });

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
