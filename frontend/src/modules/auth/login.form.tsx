import { Form, Formik } from "formik";
import Input from "../../shared/components/input";
import Button from "../../shared/components/button";

const LoginForm = ({ onSubmit, isLoginLoading }) => {
  return (
    <div className="mx-auto flex aspect-square max-h-full w-full max-w-md flex-col justify-center rounded-[3rem] bg-white px-20 py-16 md:ml-40 2xl:max-w-lg">
      <h2 className="mb-4 text-center text-3xl font-medium">ВХІД</h2>
      <Formik initialValues={{ email: '', password: '' }} onSubmit={onSubmit}>
        <Form>
          <Input label="Пошта" type="email" name="email" placeholder="Введіть пошту" required />
          <Input
            label="Пароль"
            type="password"
            name="password"
            placeholder="Введіть пароль"
            minLength={8}
            required
          />
          <Button text={'Ввійти'} disabled={isLoginLoading} />
        </Form>
      </Formik>
      <p className="text-right font-medium">
        Забули{' '}
        <a href="#" className="text-sky-500">
          пароль?
        </a>
      </p>
    </div>
  );
};

export default LoginForm;
