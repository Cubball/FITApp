import { useMutation } from 'react-query';
import { authService } from '../../services/auth/auth.service';
import { addSuccessToast, createOnError } from '../helpers/toast.helpers';
import { useNavigate } from 'react-router-dom';

interface IResetPasswordReturn {
  confirmResetPassword: (request: { id: string; token: string }) => void;
  isLoading: boolean;
}

export const useResetPassword = (): IResetPasswordReturn => {
  const navigate = useNavigate();
  const { mutateAsync: confirmResetPassword, isLoading } = useMutation({
    mutationFn: ({ id, token }: { id: string; token: string }) =>
      authService.confirmResetPassword(id, token),
    retry: false,
    onSuccess: () => {
      addSuccessToast('Пароль скинуто! Новий пароль надіслано Вам на пошту');
      navigate('/login');
    },
    onError: createOnError('Не вдалося скинути пароль')
  });
  return { confirmResetPassword, isLoading };
};
