import { createBrowserRouter } from 'react-router-dom';

export const publicRoutes = createBrowserRouter([
  {
    path: '/',
    element: <div>Hello</div>
  }
]);

export const privateRoutes = createBrowserRouter([
  {
    path: '/',
    element: <div>Hello</div>
  }
]);
