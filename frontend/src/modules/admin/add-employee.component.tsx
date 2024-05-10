import { Field, Form, Formik } from 'formik';
import { useMutation } from 'react-query';
import { NavLink, useNavigate } from 'react-router-dom';
import { IAddEmployee } from '../../services/employees/employees.types';
import { employeesService } from '../../services/employees/employees.service';
import { createOnError } from '../../shared/helpers/toast.helpers';

const AddEmployee = () => {
  // TODO: fetch roles
  const roles = [
    {
      id: '609446d3-4bbe-4efd-bff2-eae27f65017a',
      name: 'Викладач'
    },
    {
      id: '012f46da-7f4f-421d-9ba6-9537add2b9db',
      name: 'Викладач 2'
    }
  ];
  const navigate = useNavigate();

  const { mutate } = useMutation({
    mutationFn: (employee: IAddEmployee) => employeesService.addEmployee(employee),
    onSuccess: () => navigate('/employees'),
    onError: createOnError('Не вдалося додати працівника')
  })

  return (
    <div className="flex flex-col items-center gap-5 p-5">
      <h1 className="border-b border-gray-300 px-10 pb-1 text-center text-xl font-semibold">
        Додати нового співробітника
      </h1>
      <Formik
        initialValues={{
          email: '',
          roleId: ''
        }}
        onSubmit={mutate}
      >
        <Form className="flex w-full flex-col items-center gap-5">
          <Field
            id="email"
            name="email"
            type="email"
            placeholder="Введіть пошту"
            required
            className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
          />
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
};

export default AddEmployee;
