import ErrorIcon from '../../assets/icons/error-icon.svg';

const Error = () => {
  return (
    <div className="flex w-full items-center justify-center gap-3 p-10 text-xl">
      <img src={ErrorIcon} className='w-10 h-10'/>
      Під час обробки запиту виникла помилка
    </div>
  );
};

export default Error;
