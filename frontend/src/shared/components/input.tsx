import { Field } from 'formik';
import { IInputAttributes } from './types';

const Input = ({ label, ...props }: IInputAttributes) => {
  return (
    <div className="mb-4">
      <label className="font-medium" htmlFor={props.name}>
        {label}
      </label>
      <br />
      <Field className="mt-1 w-full rounded-xl border-2 border-slate-500 px-3 py-1" {...props} />
    </div>
  );
};

export default Input;
