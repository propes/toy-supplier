import { useAuth0 } from "@auth0/auth0-react";
import { LogOut, UserCircle2 } from "lucide-react";
import { Avatar, DropdownMenu } from "radix-ui";
import { FC } from "react";

const ProfileButton: FC = () => {
  const { logout, user } = useAuth0();

  return (
    <DropdownMenu.Root>
      <DropdownMenu.Trigger>
        <Avatar.Root className="flex cursor-pointer items-center rounded-full bg-slate-900 px-2 py-1 text-white hover:bg-slate-700">
          <UserCircle2 className="h-6 w-6" strokeWidth={1.2} />
          <span className="mx-2 text-sm">{user?.name}</span>
        </Avatar.Root>
      </DropdownMenu.Trigger>
      <DropdownMenu.Content className="mt-1 w-40 rounded-md border border-gray-200 bg-white shadow-lg">
        <DropdownMenu.Item
          onSelect={() =>
            logout({
              logoutParams: { returnTo: window.location.origin },
            })
          }
          className="flex cursor-pointer items-center space-x-2 px-4 py-2 text-gray-700 hover:bg-gray-100"
        >
          <LogOut className="h-4 w-4" />
          <span className="text-sm">Log Out</span>
        </DropdownMenu.Item>
      </DropdownMenu.Content>
    </DropdownMenu.Root>
  );
};

export default ProfileButton;
