import { FC } from "react";

interface ContentProps {
  children: React.ReactNode;
}

const Content: FC<ContentProps> = ({ children }) => {
  return <main className="flex-1 bg-stone-100 px-12 py-6">{children}</main>;
};

export default Content;
