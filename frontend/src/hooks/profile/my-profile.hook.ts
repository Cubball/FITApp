import { useMutation, useQuery, useQueryClient } from 'react-query';
import { QUERY_KEYS } from '../../shared/keys/query-keys';
import { authService } from '../../services/profile/profile.service';
import {
  IAddAcademicDegreesBody,
  IAddAcademicRank,
  IAddEducationBody,
  IAddPositionBody,
  IUpdateEmployeeBody
} from '../../services/profile/profile.types';
import { IUseProfileReturn } from './profile.types';

export const useMyProfile = (): IUseProfileReturn => {
  const queryClient = useQueryClient();
  const queryKey = [QUERY_KEYS.PROFILE];
  const { data, isLoading } = useQuery({
    queryKey,
    queryFn: () => authService.getProfile()
  });

  const { mutateAsync: updateProfile } = useMutation({
    mutationFn: (employeeInfo: IUpdateEmployeeBody) => authService.updateProfile(employeeInfo),
    onSuccess: () => queryClient.invalidateQueries(queryKey)
  });

  const { mutateAsync: addEducation } = useMutation({
    mutationFn: (education: IAddEducationBody) => authService.addEducation(education),
    onSuccess: () => queryClient.invalidateQueries(queryKey)
  });

  const { mutateAsync: deleteEducation } = useMutation({
    mutationFn: (index: number) => authService.deleteEducation(index.toString()),
    onSuccess: () => queryClient.invalidateQueries(queryKey)
  });

  const { mutateAsync: addAcademicRank } = useMutation({
    mutationFn: (rank: IAddAcademicRank) => authService.addAcademicRank(rank),
    onSuccess: () => queryClient.invalidateQueries(queryKey)
  });

  const { mutateAsync: deleteAcademicRank } = useMutation({
    mutationFn: (index: number) => authService.deleteAcademicRank(index.toString()),
    onSuccess: () => queryClient.invalidateQueries(queryKey)
  });

  const { mutateAsync: addAcademicDegree } = useMutation({
    mutationFn: (degree: IAddAcademicDegreesBody) => authService.addAcademicDegrees(degree),
    onSuccess: () => queryClient.invalidateQueries(queryKey)
  });

  const { mutateAsync: deleteAcademicDegree } = useMutation({
    mutationFn: (index: number) => authService.deleteAcademicDegrees(index.toString()),
    onSuccess: () => queryClient.invalidateQueries(queryKey)
  });

  const { mutateAsync: addPosition } = useMutation({
    mutationFn: (position: IAddPositionBody) => authService.addPosition(position),
    onSuccess: () => queryClient.invalidateQueries(queryKey)
  });

  const { mutateAsync: deletePosition } = useMutation({
    mutationFn: (index: number) => authService.deletePosition(index.toString()),
    onSuccess: () => queryClient.invalidateQueries(queryKey)
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
  };
};
