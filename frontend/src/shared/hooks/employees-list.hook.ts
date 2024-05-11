import { useMutation, useQuery, useQueryClient } from 'react-query';
import { QUERY_KEYS } from '../../shared/keys/query-keys';
import { employeesService } from '../../services/employees/employees.service';
import { IEmployeesPagedList } from '../../services/employees/employees.types';
import { createOnError, createOnSuccess } from '../../shared/helpers/toast.helpers';

interface IUseEmployeesListReturn {
  employeesList: IEmployeesPagedList | undefined;
  isLoading: boolean;
  deleteEmployeeById: (id: string) => void;
  resetPasswordById: (id: string) => void;
}

export const useEmployeesList = (page: number, pageSize = 10): IUseEmployeesListReturn => {
  const queryClient = useQueryClient();
  const queryKey = [QUERY_KEYS.EMPLOYEES_LIST, { page, pageSize }];
  const { data, isLoading } = useQuery({
    queryKey,
    queryFn: () => employeesService.getEmployees(page, pageSize)
  });

  const { mutateAsync: deleteEmployeeById } = useMutation({
    mutationFn: (id: string) => employeesService.deleteEmployee(id),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося видалити працівника')
  });

  const { mutateAsync: resetPasswordById } = useMutation({
    mutationFn: (id: string) => employeesService.resetEmployeePassword(id),
    onSuccess: createOnSuccess('Пароль скинуто'),
    onError: createOnError('Не вдалося скинути пароль працівника')
  });

  return { employeesList: data, isLoading, deleteEmployeeById, resetPasswordById };
};
