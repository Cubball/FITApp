import { useAuth } from '../../shared/hooks/auth.hook';
import { IAuthData } from '../../services/auth/auth.types';
import LoginForm from './login.form';

const LoginScreen = () => {
  const { handleLogin, isLoginLoading } = useAuth();
  const onFormSubmit = (values: IAuthData) => {
    handleLogin(values);
  };
  return (
    <div className="relative bg-gradient-to-r from-blue-200 font-comfortaa text-gray-800 md:overflow-hidden">
      <div className="absolute left-1/2 top-[20%] z-[-1] hidden h-screen w-screen -rotate-45 bg-document bg-contain bg-no-repeat opacity-35 md:block"></div>
      <div className="absolute -top-[80%] left-3/4 z-[-1] hidden h-screen w-screen -rotate-45 bg-document bg-contain bg-no-repeat opacity-65 md:block"></div>
      <div className="absolute -top-[30%] left-[62.5%] z-[-1] hidden h-screen w-screen -rotate-45 bg-document bg-contain bg-no-repeat md:block"></div>
      <div className="flex h-screen flex-col justify-start">
        <h1 className="w-full pt-4 text-center text-5xl font-medium md:pl-6 md:text-left">
          FITApp
        </h1>
        <div className="flex grow flex-col justify-center">
          <LoginForm onSubmit={onFormSubmit} isLoginLoading={isLoginLoading} />
        </div>
      </div>
    </div>
  );
};

export default LoginScreen;
