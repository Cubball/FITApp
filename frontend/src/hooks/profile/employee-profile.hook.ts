import { useMutation, useQuery, useQueryClient } from 'react-query';
import { QUERY_KEYS } from '../../shared/keys/query-keys';
import { IUseProfileReturn } from './profile.types';
import { toast } from 'react-toastify';
import { employeesService } from '../../services/employees/employees.service';
import {
  IAddAcademicDegreesBody,
  IAddAcademicRank,
  IAddEducationBody,
  IAddPositionBody,
  IUpdateEmployeeBody
} from '../../services/profile/profile.types';

export const useEmployeeProfile = (id: string): IUseProfileReturn => {
  const queryClient = useQueryClient();
  const queryKey = [QUERY_KEYS.EMPLOYEE_PROFILE, id];
  const createOnError = (text: string) => () => {
    toast(text, {
      type: 'error'
    });
  };
  const { data, isLoading } = useQuery({
    queryKey,
    queryFn: () => employeesService.getEmployee(id)
  });

  const { mutateAsync: updateProfile } = useMutation({
    mutationFn: (employeeInfo: IUpdateEmployeeBody) =>
      employeesService.updateEmployeeInfo(id, employeeInfo),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося оновити профіль')
  });

  const { mutateAsync: addEducation } = useMutation({
    mutationFn: (education: IAddEducationBody) => employeesService.addEducation(id, education),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося додати освіту')
  });

  const { mutateAsync: deleteEducation } = useMutation({
    mutationFn: (index: number) => employeesService.deleteEducation(id, index.toString()),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося видалити освіту')
  });

  const { mutateAsync: addAcademicRank } = useMutation({
    mutationFn: (rank: IAddAcademicRank) => employeesService.addAcademicRank(id, rank),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося додати наукове звання')
  });

  const { mutateAsync: deleteAcademicRank } = useMutation({
    mutationFn: (index: number) => employeesService.deleteAcademicRank(id, index.toString()),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося видалити наукове звання')
  });

  const { mutateAsync: addAcademicDegree } = useMutation({
    mutationFn: (degree: IAddAcademicDegreesBody) =>
      employeesService.addAcademicDegrees(id, degree),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося додати науковий ступінь')
  });

  const { mutateAsync: deleteAcademicDegree } = useMutation({
    mutationFn: (index: number) => employeesService.deleteAcademicDegrees(id, index.toString()),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося видалити науковий ступінь')
  });

  const { mutateAsync: addPosition } = useMutation({
    mutationFn: (position: IAddPositionBody) => employeesService.addPosition(id, position),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося додати посаду')
  });

  const { mutateAsync: deletePosition } = useMutation({
    mutationFn: (index: number) => employeesService.deletePosition(id, index.toString()),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося видалити посаду')
  });

  const { mutateAsync: uploadPhoto } = useMutation({
    mutationFn: (photo: File) => employeesService.uploadPhoto(id, photo),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
    onError: createOnError('Не вдалося додати фото')
  });

  const { mutateAsync: deletePhoto } = useMutation({
    mutationFn: () => employeesService.deletePhoto(id),
    onSuccess: () => queryClient.invalidateQueries(queryKey),
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
    deletePhoto
  };
};
