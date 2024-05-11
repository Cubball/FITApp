import { Outlet } from 'react-router-dom';
import Content from './content.component';
import Sidebar from './sidebar.component';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const MainLayout = () => {
  return (
    <div className="flex flex-wrap font-montserrat text-main-text text-sm">
      <Sidebar />
      <Content><Outlet /></Content>
      <ToastContainer />
    </div>
  );
};

export default MainLayout;
