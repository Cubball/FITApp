import { useNavigate } from 'react-router-dom';
import LogoutIcon from '../../assets/icons/logout-icon.svg';
import { STORAGE_KEYS } from '../../shared/keys/storage-keys';
import { useQueryClient } from 'react-query';
import { QUERY_KEYS } from '../../shared/keys/query-keys';

const LogoutButton = () => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  return (
    <button
      className="mx-auto flex w-3/4 justify-between rounded-full border border-main-text bg-link-accent p-4"
      onClick={() => {
        queryClient.setQueryData([QUERY_KEYS.AUTH], {
          accessToken: ''
        });
        localStorage.setItem(STORAGE_KEYS.JWT_TOKEN, '');
        navigate('/');
      }}
    >
      <span>Вийти</span>
      <img src={LogoutIcon} className="inline" />
    </button>
  );
};

export default LogoutButton;
