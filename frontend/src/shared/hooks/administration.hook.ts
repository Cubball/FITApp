import { useMutation, useQuery, useQueryClient } from 'react-query';
import { IAdministration } from '../../services/administration/administration.types';
import { QUERY_KEYS } from '../keys/query-keys';
import { administrationService } from '../../services/administration/administration.service';
import { addSuccessToast, createOnError } from '../helpers/toast.helpers';
import { IEmployeeShortInfo } from '../../services/employees/employees.types';
import { employeesService } from '../../services/employees/employees.service';

interface IUseAdministrationReturn {
  administration?: IAdministration;
  isAdministrationLoading: boolean;
  employees?: IEmployeeShortInfo[];
  areEmployeesLoading: boolean;
  updateAdministration: (administration: IAdministration) => void;
}

export const useAdministration = (): IUseAdministrationReturn => {
  const queryClient = useQueryClient();
  const queryKey = [QUERY_KEYS.ADMINISTRATION];
  const { data: administration, isLoading: isAdministrationLoading } = useQuery({
    queryKey,
    queryFn: () => administrationService.getAdministrationInfo()
  });
  const { mutateAsync: updateAdministration } = useMutation({
    mutationFn: (administration: IAdministration) =>
      administrationService.updateAdministrationInfo(administration),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Інформацію про адміністрацію змінено');
    },
    onError: createOnError('Не вдалося змінити інформацію про адміністрацію')
  });

  // HACK: should be able to get all the employees by omitting page and pageSize query params
  const page = 1;
  const pageSize = 10_000;
  const { data: employeesList, isLoading: areEmployeesLoading } = useQuery({
    queryKey: [QUERY_KEYS.EMPLOYEES_LIST, { page, pageSize }],
    queryFn: () => employeesService.getEmployees(page, pageSize)
  });

  return {
    administration,
    isAdministrationLoading,
    employees: employeesList?.employees,
    areEmployeesLoading,
    updateAdministration
  };
};
