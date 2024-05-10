import { Field, Form, Formik } from 'formik';
import { NavLink } from 'react-router-dom';

const AddEmployee = () => {
  // TODO: fetch roles
  const roles = [
    {
      id: 'foo',
      name: 'Викладач'
    },
    {
      id: 'bar',
      name: 'Адміністратор'
    }
  ];

  return (
    <div className="flex flex-col items-center gap-5 p-5">
      <h1 className="border-b border-gray-300 px-10 pb-1 text-center text-xl font-semibold">
        Додати нового співробітника
      </h1>
      <Formik
        initialValues={{
          email: '',
          role: ''
        }}
        onSubmit={(e) => console.log(e)}
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
            id="role"
            name="role"
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
