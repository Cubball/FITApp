import LoadingIcon from '../../assets/icons/loading-icon.svg';

const Loading = () => {
  return (
    <div className="flex w-full items-center justify-center gap-3 p-10 text-xl">
      <img src={LoadingIcon} className="h-10 w-10" />
      Завантаження
    </div>
  );
};

export default Loading;
