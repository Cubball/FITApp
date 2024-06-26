import { NavLink } from 'react-router-dom';
import RightArrowIcon from '../../assets/icons/right-arrow.svg';
import { PermissionsEnum } from '../../services/role/role.types';
import { userPermissionsService } from '../../services/auth/user-permissions.service';

interface SidebarItemProps {
  icon: string;
  text: string;
  route: string;
  requiredPermission?: PermissionsEnum;
}

const SidebarItem = ({ icon, text, route, requiredPermission }: SidebarItemProps) => {
  if (requiredPermission && !userPermissionsService.hasPermission(requiredPermission)) {
    return null;
  }

  return (
    <NavLink
    end
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
            <div>
              <img src={icon} className="mb-1 mr-1 inline" />
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
