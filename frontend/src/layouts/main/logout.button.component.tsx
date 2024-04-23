import LogoutIcon from '../../assets/icons/logout-icon.svg';

const LogoutButton = () => {
  return (
    <button className="mx-auto flex w-3/4 justify-between rounded-full border border-main-text bg-link-accent p-4">
      <span>Вийти із системи</span>
      <img src={LogoutIcon} className="inline" />
    </button>
  );
};

export default LogoutButton;
