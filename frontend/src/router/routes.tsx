import { Navigate, createBrowserRouter } from 'react-router-dom';
import LoginScreen from '../modules/auth/login.screen';
import MainLayout from '../layouts/main/main.layout';
import EmployeesList from '../modules/admin/employees-list/employees-list.component';
import MyProfile from '../modules/profile/my-profile.component';
import EmployeeProfile from '../modules/admin/employees-list/employee-profile.component';
import AddEmployee from '../modules/admin/add-employee.component';
import AddUpdateRole from '../modules/admin/add-update-role.component';
import ProtectedRoute from './protected-route';
import RolesList from '../modules/admin/roles-list/roles-list.component';
import { PermissionsEnum } from '../services/role/role.types';
import NotFound from '../shared/components/not-found';

export const publicRoutes = createBrowserRouter([
  {
    path: '/login',
    element: <LoginScreen />
  },
  {
    path: '/',
    element: <Navigate to="/login" />
  },
  {
    path: '*',
    element: <NotFound />
  }
]);

export const privateRoutes = createBrowserRouter([
  {
    path: '/',
    element: <MainLayout />,
    children: [
      {
        path: '/',
        element: <Navigate to="/profile" />
      },
      // HACK:
      {
        path: '/login',
        element: <Navigate to="/profile" />
      },
      {
        path: 'employees/new',
        element: (
          <ProtectedRoute permission={PermissionsEnum.usersCreate}>
            <AddEmployee />
          </ProtectedRoute>
        )
      },
      {
        path: 'employees/:employeeId',
        element: (
          <ProtectedRoute permission={PermissionsEnum.usersRead}>
            <EmployeeProfile />
          </ProtectedRoute>
        )
      },
      {
        path: 'employees',
        element: (
          <ProtectedRoute permission={PermissionsEnum.usersRead}>
            <EmployeesList />
          </ProtectedRoute>
        )
      },
      {
        path: 'profile',
        element: <MyProfile />
      },
      {
        path: 'roles/new',
        element: (
          <ProtectedRoute permission={PermissionsEnum.rolesCreate}>
            <AddUpdateRole />
          </ProtectedRoute>
        )
      },
      {
        path: 'roles/:roleId',
        element: (
          <ProtectedRoute permission={PermissionsEnum.rolesUpdate}>
            <AddUpdateRole />
          </ProtectedRoute>
        )
      },
      {
        path: 'roles',
        element: (
          <ProtectedRoute permission={PermissionsEnum.rolesRead}>
            <RolesList />
          </ProtectedRoute>
        )
      }
    ]
  },
  {
    path: '*',
    element: <NotFound />
  }
]);
