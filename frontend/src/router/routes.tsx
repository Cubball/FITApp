import { Navigate, createBrowserRouter } from 'react-router-dom';
import AuthScreen from '../modules/auth/auth.screen';
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
import ChangeRole from '../modules/admin/employees-list/change-role.component';
import LoginForm from '../modules/auth/login.form';
import ForgotPassword from '../modules/auth/forgot-password.component';
import ConfirmResetPassword from '../modules/auth/confirm-reset-password.component';
import ChangePassword from '../modules/profile/change-password';
import GlobalError from '../shared/components/global-error';

export const publicRoutes = createBrowserRouter([
  {
    path: '',
    children: [
      {
        path: '/login',
        element: (
          <AuthScreen>
            <LoginForm />
          </AuthScreen>
        )
      },
      {
        path: '/forgot-password',
        element: (
          <AuthScreen>
            <ForgotPassword />
          </AuthScreen>
        )
      },
      {
        path: '/reset-password-confirm',
        element: (
          <AuthScreen>
            <ConfirmResetPassword />
          </AuthScreen>
        )
      },
      {
        path: '/',
        element: <Navigate to="/login" />
      },
      {
        path: '*',
        element: <NotFound />
      }
    ],
    errorElement: <GlobalError />
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
        path: '/change-password',
        element: <ChangePassword />
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
        path: 'employees/:employeeId/role',
        element: (
          <ProtectedRoute permission={PermissionsEnum.usersUpdate}>
            <ChangeRole />
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
    ],
    errorElement: <GlobalError />
  },
  {
    path: '*',
    element: <NotFound />,
    errorElement: <GlobalError />
  }
]);
