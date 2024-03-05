import { Container } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";

import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import NavBar from "./navBar";
import { useStore } from "../stores/store";
import LoadingComponent from "./LoadingComponent";

function App() {
  const { activityStore } = useStore();

  useEffect(() => {
    activityStore.loadActivity;
  }, [activityStore]);

  if (activityStore.laodingInital)
    return <LoadingComponent content="Loading app" />;

  return (
    <>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard />
      </Container>
    </>
  );
}

export default observer(App);
