import { NavLink } from 'react-router-dom';
import RightArrowIcon from '../../assets/icons/right-arrow.svg';

const SidebarItem = ({ icon, text, route }) => {
  return (
    <NavLink
      to={route}
      className={({ isActive }) => {
        return (
          'mr-1 flex items-center justify-between p-4 pr-2' +
          (isActive ? ' bg-link-active shadow shadow-gray-400 md:rounded-r-full' : '')
        );
      }}
    >
      {({ isActive }) => {
        return (
          <>
            <div className="">
              <img src={icon} className="mb-1 inline" />
              <span className={isActive ? 'font-medium' : ''}>{text}</span>
            </div>
            <img
              src={RightArrowIcon}
              className={
                'hidden px-3 py-2.5 md:inline' + (isActive ? ' rounded-full bg-white' : '')
              }
            />
          </>
        );
      }}
    </NavLink>
  );
};

export default SidebarItem;
