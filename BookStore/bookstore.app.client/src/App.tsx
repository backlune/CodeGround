import "./App.scss";
import { useEffect, useState } from "react";
import ProductView from "./Product";

// useState

function App() {
  const [loggedIn, setLogin] = useState<boolean>(false);

  const checkClaimsAndProceed = async () => {
    // Check logged in?
    const claimsResponse = await fetch("/bff/user", {
      headers: new Headers({
        "X-CSRF": "1",
      }),
    });

    if (claimsResponse.ok) {
      const userClaims = await claimsResponse.json();

      console.log("user logged in", userClaims);
      setLogin(true);
    } else if (claimsResponse.status === 401) {
      console.log("user not logged in");
      setLogin(false);
      window.location.href = "/bff/login";
    }
    // if not logged in show login page
    // else show product page
  };

  useEffect(() => {
    checkClaimsAndProceed();
  }, []);

  return loggedIn ? <ProductView /> : <ProductView />;
}

export default App;
