import { NavLink, useParams } from "react-router-dom";
import { useEmployee } from "../../../shared/hooks/employee.hook";
import { Field, Form, Formik } from "formik";
import { useRoles } from "../../../shared/hooks/roles.hook";

const ChangeRole = () => {
  const employeeId = useParams().employeeId;
  const { employee, isLoading, changeRole } = useEmployee(employeeId);
  const { roles, isGetRolesLoading } = useRoles()
  // TODO:
  if (isLoading || isGetRolesLoading) return 'isLoading'
  if (!employeeId || !employee || !roles) return 'error';

  return (
    <div className="flex flex-col items-center gap-5 p-5">
      <h1 className="border-b border-gray-300 px-10 pb-1 text-center text-xl font-semibold">
        Змінити роль для {employee.user.email}
      </h1>
      <Formik
        initialValues={{
          roleId: employee.user.roleId,
        }}
        onSubmit={changeRole}
      >
        <Form className="flex w-full flex-col items-center gap-5">
          <Field
            id="roleId"
            name="roleId"
            as="select"
            required
            className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
          >
            <option disabled hidden value="">
              Оберіть роль
            </option>
            {roles.map((role) => (
              <option value={role.id} key={role.id}>
                {role.name}
              </option>
            ))}
          </Field>
          <button className="w-full max-w-xl rounded-md bg-main-text p-2 text-white md:w-1/2" type='submit'>
            Зберегти
          </button>
          <NavLink
            className="w-full max-w-xl rounded-md border border-main-text p-2 text-center md:w-1/2"
            to="/employees"
          >
            Скасувати
          </NavLink>
        </Form>
      </Formik>
    </div>
  );
}

export default ChangeRole
