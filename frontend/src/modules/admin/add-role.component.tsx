import { Field, Form, Formik } from 'formik';
import { NavLink } from 'react-router-dom';

const AddRole = () => {
  // TODO: fetch permissions
  const permissions = [
    {
      name: 'add_users',
      description: 'Додавати співробінтиків'
    },
    {
      name: 'delete_users',
      description: 'Видаляти співробінтиків'
    }
  ];

  return (
    <div className="flex flex-col items-center gap-5 p-5">
      <h1 className="border-b border-gray-300 px-10 pb-1 text-center text-xl font-semibold">
        Додати нову роль
      </h1>
      <Formik
        initialValues={{
          name: '',
          permissions: []
        }}
        onSubmit={(e) => console.log(e)}
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
          <button className="w-full max-w-xl rounded-md bg-main-text p-2 text-white md:w-1/2" type='submit'>
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

export default AddRole;
