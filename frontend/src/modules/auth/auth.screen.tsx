import { PropsWithChildren } from 'react';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const AuthScreen = ({ children }: PropsWithChildren) => {
  return (
    <>
      <ToastContainer />
      <div className="relative bg-gradient-to-r from-blue-200 font-comfortaa text-gray-800 md:overflow-hidden">
        <div className="absolute left-1/2 top-[20%] z-[-1] hidden h-screen w-screen -rotate-45 bg-document bg-contain bg-no-repeat opacity-35 md:block"></div>
        <div className="absolute -top-[80%] left-3/4 z-[-1] hidden h-screen w-screen -rotate-45 bg-document bg-contain bg-no-repeat opacity-65 md:block"></div>
        <div className="absolute -top-[30%] left-[62.5%] z-[-1] hidden h-screen w-screen -rotate-45 bg-document bg-contain bg-no-repeat md:block"></div>
        <div className="flex h-screen flex-col justify-start">
          <h1 className="w-full pt-4 text-center text-5xl font-medium md:pl-6 md:text-left">
            FITApp
          </h1>
          <div className="flex grow flex-col justify-center">{children}</div>
        </div>
      </div>
    </>
  );
};

export default AuthScreen;
