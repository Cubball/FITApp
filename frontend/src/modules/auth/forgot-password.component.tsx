import { Form, Formik } from 'formik';
import Input from '../../shared/components/input';
import Button from '../../shared/components/button';
import { useForgotPassword } from '../../shared/hooks/forgot-password.hook';
import { NavLink } from 'react-router-dom';

const ForgotPassword = () => {
  const { resetPassword, isLoading } = useForgotPassword();

  return (
    <div className="mx-auto flex aspect-square max-h-full w-full max-w-md flex-col justify-center rounded-[3rem] bg-white px-20 py-16 md:ml-40 2xl:max-w-lg">
      <h2 className="mb-4 text-center text-3xl font-medium">ЗАБУЛИ ПАРОЛЬ</h2>
      <Formik initialValues={{ email: '' }} onSubmit={({ email }) => resetPassword(email)}>
        <Form>
          <Input label="Пошта" type="email" name="email" placeholder="Введіть пошту" required />
          <Button text={'Скинути пароль'} disabled={isLoading} />
        </Form>
      </Formik>
      <p className="text-right font-medium">
        <NavLink to="/login" className="text-sky-500">
          Згадали пароль?
        </NavLink>
      </p>
    </div>
  );
};

export default ForgotPassword;
