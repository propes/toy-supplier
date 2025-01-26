import { withAuthenticationRequired } from "@auth0/auth0-react";
import { BrowserRouter, Route, Routes } from "react-router";
import Loading from "./components/Loading";
import PageLayout from "./components/PageLayout";
import LandingPage from "./pages/LandingPage";
import ProductsPage from "./pages/ProductsPage";

const PageLayoutWithAuth = withAuthenticationRequired(PageLayout, {
  onRedirecting: () => <Loading />,
});

const Router = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<PageLayoutWithAuth />}>
          <Route index element={<LandingPage />} />
          <Route path="products" element={<ProductsPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
};

export default Router;
