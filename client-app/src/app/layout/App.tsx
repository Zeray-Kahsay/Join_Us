import { Container } from "semantic-ui-react";
import { observer } from "mobx-react-lite";

import NavBar from "./navBar";
import { Outlet, useLocation } from "react-router-dom";
import HomePage from "../home/HomePage";

function App() {
  const location = useLocation();

  return (
    <>
      {location.pathname === "/" ? (
        <HomePage />
      ) : (
        <>
          <NavBar />
          <Container style={{ marginTop: "7em" }}>
            <Outlet />
          </Container>
        </>
      )}
    </>
  );
}

export default observer(App);
