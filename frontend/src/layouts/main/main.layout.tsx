import Content from './content.component';
import Sidebar from './sidebar.component';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { PropsWithChildren } from 'react';

const MainLayout = ({ children }: PropsWithChildren) => {
  return (
    <div className="flex flex-wrap font-montserrat text-main-text text-sm">
      <Sidebar />
      <Content>{children}</Content>
      <ToastContainer />
    </div>
  );
};

export default MainLayout;
