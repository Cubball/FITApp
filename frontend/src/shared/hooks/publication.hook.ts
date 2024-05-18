import { useMutation, useQuery, useQueryClient } from 'react-query';
import { IEmployeeShortInfo } from '../../services/employees/employees.types';
import {
  ICreateUpdatePublication,
  IPublication
} from '../../services/publications/publications.types';
import { QUERY_KEYS } from '../keys/query-keys';
import { useNavigate } from 'react-router-dom';
import { publicationsService } from '../../services/publications/publications.service';
import { employeesService } from '../../services/employees/employees.service';
import { addSuccessToast, createOnError } from '../helpers/toast.helpers';

interface IUsePublicationReturn {
  publication?: IPublication;
  isPublicationLoading: boolean;
  employees?: IEmployeeShortInfo[];
  areEmployeesLoading: boolean;
  createPublication: (publication: ICreateUpdatePublication) => void;
  updatePublication: (request: { id: string; publication: ICreateUpdatePublication }) => void;
  isCreatePublicationLoading: boolean;
  isUpdatePublicationLoading: boolean;
}

export const usePublication = (id?: string): IUsePublicationReturn => {
  const queryKey = [QUERY_KEYS.PUBLICATIONS, id];
  const queryClient = useQueryClient();
  const navigate = useNavigate();

  const { data: publication, isLoading: isPublicationLoading } = id
    ? useQuery({
      queryKey,
      queryFn: () => publicationsService.getPublication(id)
    })
    : { data: undefined, isLoading: false };

  // HACK: should be able to get all the employees by omitting page and pageSize query params
  const page = 1;
  const pageSize = 10_000;
  const { data: employeesList, isLoading: areEmployeesLoading } = useQuery({
    queryKey: [QUERY_KEYS.EMPLOYEES_LIST, { page, pageSize }],
    queryFn: () => employeesService.getEmployees(page, pageSize)
  });

  const { mutateAsync: createPublication, isLoading: isCreatePublicationLoading } = useMutation({
    mutationFn: (publication: ICreateUpdatePublication) =>
      publicationsService.addPublication(publication),
    onSuccess: () => {
      queryClient.invalidateQueries([QUERY_KEYS.PUBLICATIONS_LIST]);
      addSuccessToast('Публікацію додано');
      navigate('/publications');
    },
    onError: createOnError('Не вдалося додати публікацію')
  });

  const { mutateAsync: updatePublication, isLoading: isUpdatePublicationLoading } = useMutation({
    mutationFn: ({ id, publication }: { id: string; publication: ICreateUpdatePublication }) =>
      publicationsService.updatePublication(id, publication),
    onSuccess: () => {
      queryClient.invalidateQueries([QUERY_KEYS.PUBLICATIONS_LIST]);
      addSuccessToast('Публікацію оновлено');
      navigate('/publications');
    },
    onError: createOnError('Не вдалося оновити публікацію')
  });

  return {
    publication,
    isPublicationLoading,
    employees: employeesList?.employees,
    areEmployeesLoading,
    createPublication,
    isCreatePublicationLoading,
    updatePublication,
    isUpdatePublicationLoading
  };
};
