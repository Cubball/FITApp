import { RouteObject, createBrowserRouter } from 'react-router-dom';
import LoginScreen from '../modules/auth/login.screen';
import MainLayout from '../layouts/main/main.layout';
import EmployeesList from '../modules/admin/employees-list/employees-list.component';
import MyProfile from '../modules/profile/my-profile.component';
import EmployeeProfile from '../modules/admin/employees-list/employee-profile.component';

const sharedRoutes: RouteObject[] = [
  {
    path: '/login',
    element: <LoginScreen />
  },
  // TODO: do proper routing
  {
    path: '/',
    element: <MainLayout />,
    children: [
      {
        path: 'employees/:employeeId',
        element: <EmployeeProfile />
      },
      {
        path: 'employees',
        element: <EmployeesList />
      },
      {
        path: 'profile',
        element: <MyProfile />
      }
    ]
  },
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
