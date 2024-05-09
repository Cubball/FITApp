import { Outlet } from 'react-router-dom';
import Content from './content.component';
import Sidebar from './sidebar.component';

const MainLayout = () => {
  return (
    <div className="flex flex-wrap font-montserrat text-main-text text-sm">
      <Sidebar />
      <Content><Outlet /></Content>
    </div>
  );
};

export default MainLayout;
