import { Auth0Provider } from "@auth0/auth0-react";
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter, Route, Routes } from "react-router";
import App from "./App.tsx";

import "./index.css";

const root = document.getElementById("root")!;

createRoot(root).render(
  <StrictMode>
    <Auth0Provider
      domain="dev-bp23xcr1kyeb3ru3.us.auth0.com"
      clientId="rY5YUnUmY5SiIfya1VJjrnzeP7KDa4ON"
      authorizationParams={{
        redirect_uri: window.location.origin,
      }}
    >
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<App />} />
        </Routes>
      </BrowserRouter>
    </Auth0Provider>
  </StrictMode>,
);
