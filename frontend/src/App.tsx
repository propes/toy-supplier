import { withAuthenticationRequired } from "@auth0/auth0-react";
import { useState } from "react";
import Loading from "./components/Loading";
import "./App.css";
import LogoutButton from "./components/LogoutButton";

function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <h1>Demo app</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <LogoutButton />
      </div>
    </>
  );
}

export default withAuthenticationRequired(App, {
  onRedirecting: () => <Loading />,
});
