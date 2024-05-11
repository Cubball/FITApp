import { useEffect } from 'react';
import { useResetPassword } from '../../shared/hooks/reset-password.hook';

const ConfirmResetPassword = () => {
  const params = new URLSearchParams(location.search);
  const id = params.get('id') ?? '';
  const token = params.get('token') ?? '';
  const { confirmResetPassword } = useResetPassword();
  useEffect(() => {
    confirmResetPassword({ id, token });
  }, []);

  return (
    <div className="mx-auto flex aspect-square max-h-full w-full max-w-md flex-col justify-center rounded-[3rem] bg-white px-20 py-16 md:ml-40 2xl:max-w-lg">
      <h2 className="mb-4 text-center text-3xl font-medium">СКИДАННЯ ПАРОЛЮ</h2>
    </div>
  );
};

export default ConfirmResetPassword;
