import { Home, Shapes } from "lucide-react";
import { FC } from "react";
import { useNavigate } from "react-router";
import Content from "@/components/Content";
import NavBar from "@/components/NavBar";
import SideNav from "@/components/SideNav";

interface PageLayoutProps {
  children: React.ReactNode;
}

const PageLayout: FC<PageLayoutProps> = ({ children }) => {
  const navigate = useNavigate();

  return (
    <div className="flex h-screen flex-col">
      <NavBar />
      <div className="flex h-screen flex-row">
        <SideNav
          menuItems={[
            {
              text: "Home",
              icon: <Home />,
              onClick: () => navigate("/"),
            },
            {
              text: "Products",
              icon: <Shapes />,
              onClick: () => navigate("/products"),
            },
          ]}
        />
        <Content>{children}</Content>
      </div>
    </div>
  );
};

export default PageLayout;
