import SidebarItem from './sidebar.item.component';
import UsersIcon from '../../assets/icons/users-icon.svg';
import RolesIcon from '../../assets/icons/roles-icon.svg';
import AddUserIcon from '../../assets/icons/add-user-icon.svg'
import AddRoleIcon from '../../assets/icons/add-role-icon.svg';
import ProfileIcon from '../../assets/icons/profile-icon.svg';
import PasswordIcon from '../../assets/icons/password-icon.svg';
import LogoutButton from './logout.button.component';
import BurgerMenuIcon from '../../assets/icons/burger-menu-icon.svg';
import { useState } from 'react';
import { PermissionsEnum } from '../../services/role/role.types';

const Sidebar = () => {
  const [expanded, setExpanded] = useState(false);
  return (
    <aside className="flex shrink grow basis-[100%] flex-col justify-between gap-3 overflow-y-hidden bg-main-background py-4 md:h-screen md:basis-[20%] md:py-10">
      <button
        className="pl-4 text-left md:hidden"
        onClick={() => setExpanded((previous) => !previous)}
      >
        <img src={BurgerMenuIcon} className="mr-2 inline" />
        <span className="font-bold">Меню</span>
      </button>
      <div className={(expanded ? 'block' : 'hidden') + ' md:block'}>
        <SidebarItem icon={ProfileIcon} text="Мій профіль" route="/profile" />
        <SidebarItem icon={UsersIcon} text="Користувачі" route="/employees" requiredPermission={PermissionsEnum.usersRead}/>
        <SidebarItem icon={AddUserIcon} text="Додати користувача" route="/employees/new" requiredPermission={PermissionsEnum.usersCreate}/>
        <SidebarItem icon={RolesIcon} text="Ролі" route="/roles" requiredPermission={PermissionsEnum.rolesRead}/>
        <SidebarItem icon={AddRoleIcon} text="Додати роль" route="/roles/new" requiredPermission={PermissionsEnum.rolesCreate}/>
        <SidebarItem icon={PasswordIcon} text="Змінити пароль" route="/change-password" />
      </div>
      <div className={(expanded ? 'block' : 'hidden') + ' md:block'}>
        <LogoutButton />
      </div>
    </aside>
  );
};

export default Sidebar;
