import { useMutation } from 'react-query';
import { authService } from '../../services/auth/auth.service';
import { createOnError, createOnSuccess } from '../helpers/toast.helpers';

interface IUseForgotPasswordReturn {
  resetPassword: (email: string) => void;
  isLoading: boolean;
}

export const useForgotPassword = (): IUseForgotPasswordReturn => {
  const { mutateAsync: resetPassword, isLoading } = useMutation({
    mutationFn: (email: string) => authService.resetPassword(email),
    onSuccess: createOnSuccess('Лист для скидання паролю було надіслано'),
    onError: createOnError('Не вдалося надіслати лист для скидання паролю')
  });

  return { resetPassword, isLoading };
};
