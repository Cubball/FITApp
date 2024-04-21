import { RouterProvider } from "react-router-dom";
import { privateRoutes, publicRoutes } from "./routes";

const Router = () => {
    const isAuth = false;
    return (
        <RouterProvider router={isAuth ? privateRoutes : publicRoutes} />
    );
}

export default Router