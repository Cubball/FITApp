import { useMutation } from 'react-query';
import { createOnError, createOnSuccess } from '../helpers/toast.helpers';
import { authService } from '../../services/profile/profile.service';

interface IUseChangePasswordReturn {
  changePassword: (request: { oldPassword: string; newPassword: string }) => void;
  isLoading: boolean;
}

export const useChangePassword = (): IUseChangePasswordReturn => {
  const { mutateAsync: changePassword, isLoading } = useMutation({
    mutationFn: ({ oldPassword, newPassword }: { oldPassword: string; newPassword: string }) =>
      authService.changePassword(oldPassword, newPassword),
    onSuccess: createOnSuccess('Пароль змінено'),
    onError: createOnError('Не вдалося змінити пароль')
  });
  return { changePassword, isLoading };
};
