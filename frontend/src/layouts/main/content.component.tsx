import { PropsWithChildren } from 'react';

const Content = ({ children }: PropsWithChildren) => {
  return (
    <main className="max-h-screen shrink grow basis-[80%] md:overflow-y-auto">{children}</main>
  );
};

export default Content;
