import SidebarItem from './sidebar.item.component';
import PublicationsIcon from '../../assets/icons/publications-icon.svg';
import ProfileIcon from '../../assets/icons/profile-icon.svg';
import LogoutButton from './logout.button.component';

const Sidebar = () => {
  return (
    <aside className="sticky flex h-screen shrink grow basis-[20%] flex-col justify-between overflow-y-hidden bg-main-background py-10">
      <div>
        {/* TODO: determine routes accessible to the user and display corresponding SidebarItems */}
        <SidebarItem icon={PublicationsIcon} text="Мої публікації" route="publications" />
        <SidebarItem icon={ProfileIcon} text="Мій профіль" route="profile" />
      </div>
      <LogoutButton />
    </aside>
  );
};

export default Sidebar;
