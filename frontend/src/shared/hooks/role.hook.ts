import { useMutation, useQuery, useQueryClient } from 'react-query';
import { roleService } from '../../services/role/role.service';
import { ICreateRoleRequest, IPermission, IRole } from '../../services/role/role.types';
import { QUERY_KEYS } from '../keys/query-keys';
import { addSuccessToast } from '../helpers/toast.helpers';
import { useNavigate } from 'react-router-dom';

interface UseRoleReturn {
  role?: IRole;
  isRoleLoading: boolean;
  permissions?: IPermission[];
  handleCreateRole: (data: ICreateRoleRequest) => Promise<void>;
  handleUpdateRole: (id: string, data: ICreateRoleRequest) => Promise<void>;
  arePermissionsLoading: boolean;
  isCreateRoleLoading: boolean;
  isUpdateRoleLoading: boolean;
}

export const useRole = (id?: string): UseRoleReturn => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { data: permissions, isLoading: arePermissionsLoading } = useQuery({
    queryKey: [QUERY_KEYS.PERMISSIONS],
    queryFn: () => roleService.getPermissions()
  });

  const { data: role, isLoading: isRoleLoading } = id
    ? useQuery({
        queryKey: [QUERY_KEYS.ROLES, id],
        queryFn: () => roleService.getRole(id)
      })
    : { data: undefined, isLoading: false };

  const { mutateAsync: mutateCreateRole, isLoading: isCreateRoleLoading } = useMutation({
    mutationKey: [QUERY_KEYS.MUTATE_ROLE],
    mutationFn: (data: ICreateRoleRequest) => roleService.createRole(data),
    onSuccess: () => {
      queryClient.invalidateQueries([QUERY_KEYS.ROLES]);
      addSuccessToast('Роль додано');
      navigate('/roles');
    }
  });

  const { mutateAsync: mutateUpdateRole, isLoading: isUpdateRoleLoading } = useMutation({
    mutationKey: [QUERY_KEYS.MUTATE_ROLE],
    mutationFn: ({ id, data }: { id: string; data: ICreateRoleRequest }) =>
      roleService.updateRole(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries([QUERY_KEYS.ROLES]);
      addSuccessToast('Роль оновлено');
      navigate('/roles');
    }
  });

  const handleUpdateRole = async (id: string, data: ICreateRoleRequest) => {
    await mutateUpdateRole({ id, data });
  };

  const handleCreateRole = async (data: ICreateRoleRequest) => {
    await mutateCreateRole(data);
  };

  return {
    role,
    isRoleLoading,
    permissions,
    handleCreateRole,
    handleUpdateRole,
    arePermissionsLoading,
    isCreateRoleLoading,
    isUpdateRoleLoading
  };
};
