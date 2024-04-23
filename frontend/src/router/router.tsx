import { RouterProvider } from 'react-router-dom';
import { privateRoutes, publicRoutes } from './routes';
import { useAuth } from '../shared/hooks/auth.hook';

const Router = () => {
  const { authData, isRefreshLoading } = useAuth();

  // TODO: handle loading properly
  if (isRefreshLoading) {
    return <div>Loading...</div>;
  }

  return <RouterProvider router={authData?.accessToken ? privateRoutes : publicRoutes} />;
};

export default Router;
