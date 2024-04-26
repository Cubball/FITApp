import Content from './content.component';
import Sidebar from './sidebar.component';

const MainLayout = ({ children }) => {
  return (
    <div className="flex flex-wrap font-montserrat text-main-text text-sm">
      <Sidebar />
      <Content>{children}</Content>
    </div>
  );
};

export default MainLayout;
