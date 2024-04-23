import Content from "./content.component"
import Sidebar from './sidebar.component';

const MainLayout = ({ children }) => {
  return (
    <div className="flex flex-wrap text-main-text">
      <Sidebar />
      <Content>{children}</Content>
    </div>
  );
};

export default MainLayout;
