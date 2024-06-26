import { ButtonHTMLAttributes } from 'react';

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  text: string;
}

const Button = ({ text, ...props }: ButtonProps) => {
  return (
    <button
      className={
        props.disabled
          ? 'mb-2 mt-3 w-full rounded-xl bg-gray-400 py-1.5 font-medium text-white'
          : 'mb-2 mt-3 w-full rounded-xl bg-sky-500 py-1.5 font-medium text-white hover:bg-sky-600'
      }
      type={props.type ?? 'submit'}
      {...props}
    >
      {text}
    </button>
  );
};

export default Button;
