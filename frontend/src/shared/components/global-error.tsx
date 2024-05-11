import { NavLink } from "react-router-dom";

const GlobalError = () => {
  return (
    <div className="h-screen w-full bg-gradient-to-r from-blue-200 font-comfortaa text-gray-800">
      <h1 className="w-full pt-4 text-center text-5xl font-medium md:pl-6 md:text-left">FITApp</h1>
      <div className="flex flex-col items-center justify-center gap-5">
        <h1 className="text-3xl font-bold">Помилка!</h1>
        <p>Під час обробки запиту виникла помилка</p>
        <NavLink to='/' className="text-white bg-main-text p-3 rounded-md">Повернутися назад</NavLink>
      </div>
    </div>
  );
}

export default GlobalError
