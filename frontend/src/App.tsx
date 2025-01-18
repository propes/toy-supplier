import { withAuthenticationRequired } from "@auth0/auth0-react";
import Loading from "./components/Loading";
import PageLayout from "./components/PageLayout";
import LandingPage from "./pages/LandingPage";

const App = () => {
  return (
    <PageLayout>
      <LandingPage />
    </PageLayout>
  );
};

export default withAuthenticationRequired(App, {
  onRedirecting: () => <Loading />,
});
