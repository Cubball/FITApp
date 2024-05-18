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
import PublicationsList from '../modules/publications/publications-list.component';
import AddUpdatePublication from '../modules/publications/add-update-publication.component';
import Reports from '../modules/publications/reports.component';

export const publicRoutes = createBrowserRouter([
  {
    path: '/login',
    element: (
      <AuthScreen>
        <LoginForm />
      </AuthScreen>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '/forgot-password',
    element: (
      <AuthScreen>
        <ForgotPassword />
      </AuthScreen>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '/reset-password-confirm',
    element: (
      <AuthScreen>
        <ConfirmResetPassword />
      </AuthScreen>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '/',
    element: <Navigate to="/login" />,
    errorElement: <GlobalError />
  },
  {
    path: '*',
    element: <NotFound />,
    errorElement: <GlobalError />
  }
]);

export const privateRoutes = createBrowserRouter([
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
    element: (
      <MainLayout>
        <ChangePassword />
      </MainLayout>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '/employees/new',
    element: (
      <MainLayout>
        <ProtectedRoute permission={PermissionsEnum.usersCreate}>
          <AddEmployee />
        </ProtectedRoute>
      </MainLayout>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '/employees/:employeeId',
    element: (
      <MainLayout>
        <ProtectedRoute permission={PermissionsEnum.usersRead}>
          <EmployeeProfile />
        </ProtectedRoute>
      </MainLayout>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '/employees/:employeeId/role',
    element: (
      <MainLayout>
        <ProtectedRoute permission={PermissionsEnum.usersUpdate}>
          <ChangeRole />
        </ProtectedRoute>
      </MainLayout>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '/employees',
    element: (
      <MainLayout>
        <ProtectedRoute permission={PermissionsEnum.usersRead}>
          <EmployeesList />
        </ProtectedRoute>
      </MainLayout>
    ),
    errorElement: <GlobalError />,
  },
  {
    path: '/profile',
    element: (
      <MainLayout>
        <MyProfile />
      </MainLayout>
    ),
    errorElement: <GlobalError />,
  },
  {
    path: '/reports',
    element: (
      <MainLayout>
        <Reports />
      </MainLayout>
    ),
    errorElement: <GlobalError />,
  },
  {
    path: '/publications/new',
    element: (
      <MainLayout>
        <AddUpdatePublication />
      </MainLayout>
    ),
    errorElement: <GlobalError />,
  },
  {
    path: '/publications/:publicationId',
    element: (
      <MainLayout>
        <AddUpdatePublication />
      </MainLayout>
    ),
    errorElement: <GlobalError />,
  },
  {
    path: '/publications',
    element: (
      <MainLayout>
        <PublicationsList />
      </MainLayout>
    ),
    errorElement: <GlobalError />,
  },
  {
    path: '/roles/new',
    element: (
      <MainLayout>
        <ProtectedRoute permission={PermissionsEnum.rolesCreate}>
          <AddUpdateRole />
        </ProtectedRoute>
      </MainLayout>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '/roles/:roleId',
    element: (
      <MainLayout>
        <ProtectedRoute permission={PermissionsEnum.rolesUpdate}>
          <AddUpdateRole />
        </ProtectedRoute>
      </MainLayout>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '/roles',
    element: (
      <MainLayout>
        <ProtectedRoute permission={PermissionsEnum.rolesRead}>
          <RolesList />
        </ProtectedRoute>
      </MainLayout>
    ),
    errorElement: <GlobalError />
  },
  {
    path: '*',
    element: <NotFound />,
    errorElement: <GlobalError />
  }
]);
