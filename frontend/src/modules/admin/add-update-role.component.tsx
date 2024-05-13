import { Field, Form, Formik } from 'formik';
import { NavLink, useParams } from 'react-router-dom';
import { useRole } from '../../shared/hooks/role.hook';
import { ICreateRoleRequest } from '../../services/role/role.types';
import Loading from '../../shared/components/loading';
import Error from '../../shared/components/error';

const AddUpdateRole = () => {
  const params = useParams();
  const id = params.roleId;

  const {
    role,
    isRoleLoading,
    permissions,
    arePermissionsLoading,
    isCreateRoleLoading,
    isUpdateRoleLoading,
    handleCreateRole,
    handleUpdateRole
  } = useRole(id);
  if (arePermissionsLoading || isRoleLoading) {
    return <Loading />;
  }

  if (!permissions) {
    return <Error />;
  }

  return (
    <div className="flex flex-col items-center gap-5 p-5">
      <h1 className="border-b border-gray-300 px-10 pb-1 text-center text-xl font-semibold">
        {role ? 'Оновити роль' : 'Додати нову роль'}
      </h1>
      <Formik
        initialValues={{
          name: role?.name ?? '',
          permissions: role?.permissions ?? []
        }}
        onSubmit={(createUpdateRole) => {
          if (role) {
            handleUpdateRole(role.id, createUpdateRole as ICreateRoleRequest);
          } else {
            handleCreateRole(createUpdateRole as ICreateRoleRequest);
          }
        }}
      >
        <Form className="flex w-full flex-col items-center gap-5">
          <Field
            id="name"
            name="name"
            placeholder="Введіть назву"
            required
            className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
          />
          <div className="w-full max-w-xl font-semibold md:w-1/2">
            Дозволи для ролі:
            <div className="mt-1 flex flex-col gap-5 rounded-md border border-gray-300 p-3">
              {permissions.map((permission) => (
                <label className="flex items-center" key={permission.name}>
                  <Field
                    type="checkbox"
                    name="permissions"
                    value={permission.name}
                    className="mr-1 h-4 w-4"
                  />
                  {permission.description}
                </label>
              ))}
            </div>
          </div>
          <button
            className="w-full max-w-xl rounded-md bg-main-text p-2 text-white disabled:bg-gray-400 md:w-1/2"
            type="submit"
            disabled={isUpdateRoleLoading || isCreateRoleLoading}
          >
            Зберегти
          </button>
          <NavLink
            className="w-full max-w-xl rounded-md border border-main-text p-2 text-center md:w-1/2"
            to="/roles"
          >
            Скасувати
          </NavLink>
        </Form>
      </Formik>
    </div>
  );
};

export default AddUpdateRole;
