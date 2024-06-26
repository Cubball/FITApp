import { useMutation, useQuery, useQueryClient } from 'react-query';
import { QUERY_KEYS } from '../../../shared/keys/query-keys';
import { authService } from '../../../services/profile/profile.service';
import {
  IAddAcademicDegreesBody,
  IAddAcademicRank,
  IAddEducationBody,
  IAddPositionBody,
  IUpdateEmployeeBody
} from '../../../services/profile/profile.types';
import { addInfoToast, addSuccessToast, createOnError } from '../../../shared/helpers/toast.helpers';
import { IUseProfileReturn } from './profile.hook';

export const useMyProfile = (): IUseProfileReturn => {
  const queryClient = useQueryClient();
  const queryKey = [QUERY_KEYS.PROFILE];
  const { data, isLoading } = useQuery({
    queryKey,
    queryFn: () => authService.getProfile()
  });

  const { mutateAsync: updateProfile } = useMutation({
    mutationFn: (employeeInfo: IUpdateEmployeeBody) => authService.updateProfile(employeeInfo),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Профіль оновлено');
    },
    onError: createOnError('Не вдалося оновити профіль')
  });

  const { mutateAsync: addEducation } = useMutation({
    mutationFn: (education: IAddEducationBody) => authService.addEducation(education),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Освіту додано');
    },
    onError: createOnError('Не вдалося додати освіту')
  });

  const { mutateAsync: deleteEducation } = useMutation({
    mutationFn: (index: number) => authService.deleteEducation(index.toString()),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Освіту видалено');
    },
    onError: createOnError('Не вдалося видалити освіту')
  });

  const { mutateAsync: addAcademicRank } = useMutation({
    mutationFn: (rank: IAddAcademicRank) => authService.addAcademicRank(rank),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Наукове звання додано');
    },
    onError: createOnError('Не вдалося додати наукове звання')
  });

  const { mutateAsync: deleteAcademicRank } = useMutation({
    mutationFn: (index: number) => authService.deleteAcademicRank(index.toString()),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Наукове звання видалено');
    },
    onError: createOnError('Не вдалося видалити наукове звання')
  });

  const { mutateAsync: addAcademicDegree } = useMutation({
    mutationFn: (degree: IAddAcademicDegreesBody) => authService.addAcademicDegrees(degree),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Науковий ступінь додано');
    },
    onError: createOnError('Не вдалося додати науковий ступінь')
  });

  const { mutateAsync: deleteAcademicDegree } = useMutation({
    mutationFn: (index: number) => authService.deleteAcademicDegrees(index.toString()),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Науковий ступінь видалено');
    },
    onError: createOnError('Не вдалося видалити науковий ступінь')
  });

  const { mutateAsync: addPosition } = useMutation({
    mutationFn: (position: IAddPositionBody) => authService.addPosition(position),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Посаду додано');
    },
    onError: createOnError('Не вдалося додати посаду')
  });

  const { mutateAsync: deletePosition } = useMutation({
    mutationFn: (index: number) => authService.deletePosition(index.toString()),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Посаду видалено');
    },
    onError: createOnError('Не вдалося видалити посаду')
  });

  const { mutateAsync: uploadPhoto } = useMutation({
    mutationFn: (photo: File) => authService.uploadPhoto(photo),
    onMutate: () => addInfoToast('Фото завантажується...'),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Фото додано');
    },
    onError: createOnError('Не вдалося додати фото')
  });

  const { mutateAsync: deletePhoto } = useMutation({
    mutationFn: () => authService.deletePhoto(),
    onSuccess: () => {
      queryClient.invalidateQueries(queryKey);
      addSuccessToast('Фото видалено');
    },
    onError: createOnError('Не вдалося видалити фото')
  });

  return {
    profile: data,
    isLoading,
    updateProfile,
    addEducation,
    deleteEducation,
    addAcademicRank,
    deleteAcademicRank,
    addAcademicDegree,
    deleteAcademicDegree,
    addPosition,
    deletePosition,
    uploadPhoto,
    deletePhoto,
  };
};
