import "./App.scss";
import { useEffect, useState } from "react";
import ProductView from "./Product";

// useState
export interface ClaimsType {
  type: string;
  value: string;
}

function App() {
  // const [loggedIn, setLogin] = useState<boolean>(false);
  const [userClaims, setUserClaims] = useState<ClaimsType[] | undefined>(
    undefined
  );

  const checkClaimsAndProceed = async () => {
    // Check logged in?
    const claimsResponse = await fetch("/bff/user", {
      headers: new Headers({
        "X-CSRF": "1",
      }),
    });

    if (claimsResponse.ok) {
      const claims = await claimsResponse.json();

      console.log("user logged in", claims);
      setUserClaims(claims);
    } else if (claimsResponse.status === 401) {
      console.log("user not logged in");
    }
    // if not logged in show login page
    // else show product page
  };

  const logout = async () => {
    if (userClaims) {
      const logoutUrl = userClaims.find(
        (claim) => claim.type === "bff:logout_url"
      ).value;
      window.location.href = logoutUrl;
    } else {
      window.location.href = "/bff/logout";
    }
  };

  const login = async () => {
    window.location.href = "/bff/login";
  };

  useEffect(() => {
    checkClaimsAndProceed();
  }, []);

  return (
    <div>
      <div className="d-flex flex-row-reverse">
        <div className="p-2">
          {userClaims ? (
            <button onClick={logout} className="btn btn-secondary">
              Log out
            </button>
          ) : (
            <button onClick={login} className="btn btn-primary">
              Log in
            </button>
          )}
        </div>
      </div>
      <div>{userClaims ? <ProductView /> : null}</div>
    </div>
  );
}

export default App;
