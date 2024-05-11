import Loading from './loading';

const GlobalLoading = () => {
  return (
    <div className="h-screen w-full bg-gradient-to-r from-blue-200 font-comfortaa text-gray-800">
      <h1 className="w-full pt-4 text-center text-5xl font-medium md:pl-6 md:text-left">FITApp</h1>
      <div className="flex flex-col items-center justify-center gap-5">
        <Loading />
      </div>
    </div>
  );
};

export default GlobalLoading;
