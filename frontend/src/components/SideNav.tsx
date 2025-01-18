import { ChevronLeft, ChevronRight } from "lucide-react";
import { FC, useEffect, useState } from "react";

interface SideNavMenuItem {
  text: string;
  icon: React.ReactNode;
  onClick?: () => void;
}

interface SideNavProps {
  menuItems: SideNavMenuItem[];
}

const SideNav: FC<SideNavProps> = ({ menuItems }) => {
  const [isCollapsed, setIsCollapsed] = useState(false);

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth <= 1024) {
        setIsCollapsed(true);
      } else {
        setIsCollapsed(false);
      }
    };

    handleResize();

    window.addEventListener("resize", handleResize);

    return () => window.removeEventListener("resize", handleResize);
  }, []);

  return (
    <aside
      className={`bg-slate-900 text-white ${isCollapsed ? "w-16" : "w-64"} transition-all duration-500 ease-in-out`}
    >
      <div className="mb-2 flex justify-end p-2">
        <button
          className="cursor-pointer self-end rounded"
          onClick={() => setIsCollapsed(!isCollapsed)}
        >
          {isCollapsed ? (
            <div className="flex">
              <span className="flex items-center text-xs">OPEN</span>
              <ChevronRight className="h-5 w-5" strokeWidth={1.2} />
            </div>
          ) : (
            <div className="flex">
              <ChevronLeft className="h-5 w-5" strokeWidth={1.2} />
              <span className="flex items-center text-xs">CLOSE</span>
            </div>
          )}
        </button>
      </div>
      <nav>
        <ul>
          {menuItems.map((item, index) => (
            <li key={index}>
              <SideNavMenuButton
                text={item.text}
                isCollapsed={isCollapsed}
                icon={item.icon}
                onClick={item.onClick}
              />
            </li>
          ))}
        </ul>
      </nav>
    </aside>
  );
};

interface SideNavMenuButtonProps {
  text: string;
  isCollapsed: boolean;
  icon: React.ReactNode;
  onClick?: () => void;
}

const SideNavMenuButton: FC<SideNavMenuButtonProps> = ({
  text,
  isCollapsed,
  icon,
  onClick,
}) => {
  return (
    <button
      className="w-full cursor-pointer p-4 hover:bg-slate-700"
      onClick={onClick}
    >
      <div className="flex items-center">
        <div className={`flex items-center justify-center`}>
          {/* This containing div prevents the icon from being repositioned
              when the collapsed sidenav is expanded */}
          {icon}
        </div>
        <span
          className={`ml-2 transition-all duration-300 ease-in-out ${
            isCollapsed ? "w-0 opacity-0" : "w-auto opacity-100"
          }`}
        >
          {text}
        </span>
      </div>
    </button>
  );
};

export default SideNav;
