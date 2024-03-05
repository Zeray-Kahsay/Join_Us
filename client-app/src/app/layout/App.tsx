import { Container } from "semantic-ui-react";
import { observer } from "mobx-react-lite";

import NavBar from "./navBar";
import { Outlet } from "react-router-dom";

function App() {
  return (
    <>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <Outlet />
      </Container>
    </>
  );
}

export default observer(App);
