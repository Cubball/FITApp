import { RouteObject, createBrowserRouter } from 'react-router-dom';
import LoginScreen from '../modules/auth/login.screen';
import MainLayout from '../layouts/main/main.layout';
import EmployeesList from '../modules/admin/employees-list/employees-list.component';
import MyProfile from '../modules/profile/my-profile.component';
import EmployeeProfile from '../modules/admin/employees-list/employee-profile.component';
import AddEmployee from '../modules/admin/add-employee.component';
import NotFound from '../shared/components/not-found';
import AddUpdateRole from '../modules/admin/add-update-role.component';
import RolesList from '../modules/admin/roles-list/roles-list.component';

// TODO: do proper routing
const sharedRoutes: RouteObject[] = [
  {
    path: '/',
    errorElement: <NotFound />
  },
  {
    path: 'login',
    element: <LoginScreen />
  }
];

export const publicRoutes = createBrowserRouter(sharedRoutes);

export const privateRoutes = createBrowserRouter([
  {
    path: '',
    element: <MainLayout />,
    children: [
      {
        path: 'employees/new',
        element: <AddEmployee />
      },
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
      },
      {
        path: 'roles/new',
        element: <AddUpdateRole />
      },
      {
        path: 'roles/:roleId',
        element: <AddUpdateRole />
      },
      {
        path: 'roles',
        element: <RolesList />
      }
    ]
  },
  ...sharedRoutes
]);
