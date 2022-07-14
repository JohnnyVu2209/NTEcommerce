import { Navigate, useRoutes } from 'react-router-dom';
import { useSelector } from 'react-redux';
// layouts
import DashboardLayout from './layouts/dashboard';
import LogoOnlyLayout from './layouts/LogoOnlyLayout';
//
import Category from './pages/Category/CategoriesList';
import CreateCategory from './pages/Category/CreateCategory';
import Blog from './pages/Blog';
import User from './pages/User';
import Login from './pages/Login';
import NotFound from './pages/Page404';
import Register from './pages/Register';
import Products from './pages/Products';
import DashboardApp from './pages/DashboardApp';
import CategoryDetail from './pages/Category/CategoryDetail';
import ProductList from './pages/Product/ProductList';
import CreateProduct from './pages/Product/CreateProduct';
import ProductDetail from './pages/Product/ProductDetail';
import EditProduct from './pages/Product/EditProduct';

// ----------------------------------------------------------------------

export default function Router() {
  const { isAuth } = useSelector((state) => state.auth);
  return useRoutes([
    {
      path: '/dashboard',
      element: isAuth ?  <DashboardLayout /> : <Navigate to="/login"/>,
      children: [
        { path: 'app', element: <DashboardApp /> },
        { path: 'user', element: <User /> },
        { path: 'products', element: <ProductList /> },
        { path: 'products/create', element: <CreateProduct /> },
        { path: 'products/detail/:id', element: <ProductDetail /> },
        { path: 'products/edit/:id', element: <EditProduct /> },
        { path: 'blog', element: <Blog /> },
        {
          path: 'category',
          element: <Category />
        },
        {
          path: 'category/create',
          element: <CreateCategory />,
        },
        {
          path: 'category/detail/:id',
          element: <CategoryDetail />,
        },
      ],
    },
    {
      path: '/',
      element: <LogoOnlyLayout />,
      children: [
        { path: '/', element: isAuth ?  <Navigate to="/dashboard/app"/> : <Navigate to="login"/> },
        { path: 'login', element: <Login /> },
        { path: 'register', element: <Register /> },
        { path: '404', element: <NotFound /> },
        { path: '*', element: <Navigate to="/404" /> },
      ],
    },
    { path: '*', element: <Navigate to="/404" replace /> },
  ]);
}
