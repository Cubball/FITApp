import { Field, Form, Formik } from 'formik';
import { NavLink } from 'react-router-dom';
import { useChangePassword } from '../../shared/hooks/change-password.hook';

const ChangePassword = () => {
  const { changePassword, isLoading } = useChangePassword();
  return (
    <div className="flex flex-col items-center gap-5 p-5">
      <h1 className="border-b border-gray-300 px-10 pb-1 text-center text-xl font-semibold">
        Змінити пароль
      </h1>
      <Formik
        initialValues={{
          oldPassword: '',
          newPassword: '',
          newPasswordConfirm: ''
        }}
        onSubmit={({ oldPassword, newPassword }) => changePassword({ oldPassword, newPassword })}
        validate={(values) =>
          values.newPassword !== values.newPasswordConfirm
            ? { newPasswordConfirm: 'Паролі не співпадають' }
            : {}
        }
      >
        {({ errors, touched }) => (
          <Form className="flex w-full flex-col items-center gap-5">
            <Field
              id="oldPassword"
              name="oldPassword"
              placeholder="Введіть поточний пароль"
              required
              type="password"
              className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
              minLength="8"
            />
            <Field
              id="newPassword"
              name="newPassword"
              placeholder="Введіть новий пароль"
              required
              type="password"
              className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
              minLength="8"
            />
            <Field
              id="newPasswordConfirm"
              name="newPasswordConfirm"
              placeholder="Введіть новий пароль ще раз"
              required
              type="password"
              className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
              minLength="8"
            />
            {errors.newPasswordConfirm && touched.newPasswordConfirm ? (
              <span className="text-red-500">{errors.newPasswordConfirm}</span>
            ) : null}
            <button
              className="w-full max-w-xl rounded-md bg-main-text p-2 text-white md:w-1/2"
              type="submit"
              disabled={isLoading}
            >
              Змінити
            </button>
            <NavLink
              className="w-full max-w-xl rounded-md border border-main-text p-2 text-center md:w-1/2"
              to="/profile"
            >
              Скасувати
            </NavLink>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default ChangePassword;
