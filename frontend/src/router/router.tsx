import { RouterProvider } from 'react-router-dom';
import { privateRoutes, publicRoutes } from './routes';
import { useAuth } from '../shared/hooks/auth.hook';
import GlobalLoading from '../shared/components/global-loading';

const Router = () => {
  const { authData, isRefreshLoading } = useAuth();

  if (isRefreshLoading) {
    return <GlobalLoading />
  }

  return <RouterProvider router={authData?.accessToken ? privateRoutes : publicRoutes} />;
};

export default Router;
