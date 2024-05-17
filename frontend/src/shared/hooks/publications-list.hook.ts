import { useMutation, useQuery, useQueryClient } from 'react-query';
import { IPublicationsPagedList } from '../../services/publications/publications.types';
import { QUERY_KEYS } from '../keys/query-keys';
import { publicationsService } from '../../services/publications/publications.service';
import { addSuccessToast, createOnError } from '../helpers/toast.helpers';

interface IUsePublicationsListReturn {
  publicationsList?: IPublicationsPagedList;
  isLoading: boolean;
  deletePublicationById: (id: string) => void;
}

export const usePublicationsList = (page: number, pageSize = 10): IUsePublicationsListReturn => {
  const queryClient = useQueryClient();
  const queryKey = [QUERY_KEYS.PUBLICATIONS_LIST, { page, pageSize }];
  const { data: publicationsList, isLoading } = useQuery({
    queryKey,
    queryFn: () => publicationsService.getPublications(page, pageSize)
  });

  const { mutateAsync: deletePublicationById } = useMutation({
    mutationFn: (id: string) => publicationsService.deletePublication(id),
    onSuccess: () => {
      addSuccessToast('Публікацію видалено');
      queryClient.invalidateQueries(queryKey);
    },
    onError: createOnError('Не вдалося видалити публікацію')
  });

  return { publicationsList, isLoading, deletePublicationById };
};
