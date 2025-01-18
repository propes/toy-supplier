import { useNavigate } from "react-router";
import ProfileButton from "./ProfileButton";

const NavBar = () => {
  const navigate = useNavigate();

  return (
    <header className="flex h-16 items-center justify-between border-b border-gray-200 bg-white px-6 py-2 shadow">
      <h1
        className="cursor-pointer text-xl font-medium"
        onClick={() => navigate("/")}
      >
        Toys App
      </h1>
      <ProfileButton />
    </header>
  );
};

export default NavBar;
