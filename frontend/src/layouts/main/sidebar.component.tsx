import SidebarItem from './sidebar.item.component';
import PublicationsIcon from '../../assets/icons/publications-icon.svg';
import ProfileIcon from '../../assets/icons/profile-icon.svg';
import LogoutButton from './logout.button.component';
import BurgerMenuIcon from '../../assets/icons/burger-menu-icon.svg';
import { useState } from 'react';

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
        {/* TODO: determine routes accessible to the user and display corresponding SidebarItems */}
        <SidebarItem icon={PublicationsIcon} text="Мої публікації" route="publications" />
        <SidebarItem icon={ProfileIcon} text="Мій профіль" route="profile" />
      </div>
      <div className={(expanded ? 'block' : 'hidden') + ' md:block'}>
        <LogoutButton />
      </div>
    </aside>
  );
};

export default Sidebar;
