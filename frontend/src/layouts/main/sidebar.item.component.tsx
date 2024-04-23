import { NavLink } from 'react-router-dom';
import RightArrowIcon from '../../assets/icons/right-arrow.svg';

const SidebarItem = ({ icon, text, route }) => {
  return (
    <NavLink
      to={route}
      className={({ isActive }) => {
        return (
          'mr-1 flex items-center justify-between p-4 pr-2' +
          (isActive ? ' rounded-r-full bg-link-active shadow shadow-gray-400' : '')
        );
      }}
    >
      {({ isActive }) => {
        return (
          <>
            <div>
              <img src={icon} className="mb-1 inline" />
              <span className={isActive ? 'font-medium' : ''}>{text}</span>
            </div>
            <img
              src={RightArrowIcon}
              className={'inline px-3 py-2.5' + (isActive ? ' rounded-full bg-white' : '')}
            />
          </>
        );
      }}
    </NavLink>
  );
};

export default SidebarItem;
