import { RouteObject, createBrowserRouter } from 'react-router-dom';
import LoginScreen from '../modules/auth/login.screen';

const sharedRoutes: RouteObject[] = [
  {
    path: '/login',
    element: <LoginScreen />
  }
];

export const publicRoutes = createBrowserRouter([
  {
    path: '/',
    element: <div>Hello</div>
  },
  ...sharedRoutes
]);

export const privateRoutes = createBrowserRouter([
  {
    path: '/',
    element: <div>Hello</div>
  },
  ...sharedRoutes
]);
