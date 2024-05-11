import { useMutation, useQuery, useQueryClient } from 'react-query';
import { IEmployee } from '../../services/profile/profile.types';
import { QUERY_KEYS } from '../keys/query-keys';
import { employeesService } from '../../services/employees/employees.service';
import { addSuccessToast, createOnError } from '../helpers/toast.helpers';

interface IUseEmployeeReturn {
  employee?: IEmployee;
  isLoading: boolean;
  changeRole: (request: { roleId: string }) => void;
}

export const useEmployee = (id?: string): IUseEmployeeReturn => {
  const queryClient = useQueryClient();
  const queryKey = [QUERY_KEYS.EMPLOYEE, id];
  const { data: employee, isLoading } = useQuery({
    queryKey,
    queryFn: () => employeesService.getEmployee(id ?? '')
  });

  const { mutateAsync: changeRole } = useMutation({
    mutationFn: (request: { roleId: string }) =>
      employeesService.changeEmployeeRole(id ?? '', request.roleId),
    onError: createOnError('Не вдалося змінити роль'),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Роль змінено');
    }
  });

  return { employee, isLoading, changeRole };
};
